using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Channels;
using Wumpex.Net.Core.Configurations;
using Wumpex.Net.Core.Exceptions;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Dispatch;

/// <summary>
/// A <see cref="BackgroundService" /> that functions as a worker pool for dispatch events.
/// </summary>
internal sealed class HostedWorkerPool : BackgroundService
{
	private readonly ILogger<HostedWorkerPool> _logger;
	private readonly IOptionsMonitor<DiscordConfiguration> _monitor;
	private readonly Channel<(EventContext Context, Func<CancellationToken, ValueTask> Work)> _events;

	public HostedWorkerPool(ILogger<HostedWorkerPool> logger, IOptionsMonitor<DiscordConfiguration> monitor)
	{
		_logger = logger;
		_monitor = monitor;
		_events = Channel.CreateUnbounded<(EventContext Context, Func<CancellationToken, ValueTask> Work)>(new UnboundedChannelOptions
		{
			SingleWriter = true
		});
	}

	/// <summary>
	/// Enqueue <paramref name="dispatch" /> to be executed by the worker pool.
	/// </summary>
	public ValueTask QueueDispatch(Func<CancellationToken, ValueTask> dispatch, EventContext context, CancellationToken cancellationToken)
		=> _events.Writer.WriteAsync((context, dispatch), cancellationToken);

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		// TODO listen for worker count changes on _monitor and update the worker pool.
		var workers = new List<Task>(_monitor.CurrentValue.Gateway.WorkerCount);
		for (int i = 0; i < _monitor.CurrentValue.Gateway.WorkerCount; i++)
			workers.Add(QueueWorker(i, stoppingToken));

		await Task.WhenAll(workers);
		await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
	}

	/// <summary>
	/// The worker task that handles incoming dispatches in the background.
	/// </summary>
	private async Task QueueWorker(int id, CancellationToken cancellationToken)
	{
		var stopwatch = new Stopwatch();
		using var _ = _logger.BeginScope(new { Worker = id });

		await foreach (var (context, dispatch) in _events.Reader.ReadAllAsync(cancellationToken))
		{
			try
			{
				stopwatch.Restart();
				await dispatch(cancellationToken);

				_logger.LogTrace("Finished processing {EventName} {Sequence} after {Elapsed}", context.EventName, context.Sequence, stopwatch.Elapsed);
			}
			catch (Exception exception) when (exception is not DiscordException { CanRecover: true })
			{
				_logger.LogError(exception, "An error occured processing {EventName} {Sequence} after {Elapsed}", context.EventName, context.Sequence, stopwatch.Elapsed);
			}
		}
	}
}