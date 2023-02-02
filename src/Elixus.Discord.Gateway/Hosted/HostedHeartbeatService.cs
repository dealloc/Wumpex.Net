using Elixus.Discord.Gateway.Contracts;
using Elixus.Discord.Gateway.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elixus.Discord.Gateway.Hosted;

/// <summary>
/// Responsible for sending out heartbeats, managing the sequence and checking for ACK's.
/// </summary>
/// <seealso cref="IHeartbeatService" />
internal sealed class HostedHeartbeatService : BackgroundService, IHeartbeatService
{
	/// <inheritdoc cref="IHeartbeatService.Sequence" />
	public ValueTask<int?> Sequence => ValueTask.FromResult(_sequence);

	private TimeSpan _interval = Timeout.InfiniteTimeSpan;
	private int? _sequence = null;
	private bool _wasAcknowledged = false;
	// used when the service needs to (potentially) wait for something.
	// we use this to interrupt waits (such as initial start, immediate heartbeat, ...)
	private CancellationTokenSource _waitHandle = new();
	private readonly ILogger<HostedHeartbeatService> _logger;
	private readonly IDiscordGateway _discordGateway;

	public HostedHeartbeatService(ILogger<HostedHeartbeatService> logger, IDiscordGateway discordGateway)
	{
		_logger = logger;
		_discordGateway = discordGateway;
	}

	/// <inheritdoc cref="IHeartbeatService.Start" />
	public ValueTask Start(int interval, CancellationToken cancellationToken = default)
	{
		_sequence = 0;
		_wasAcknowledged = true;
		_interval = TimeSpan.FromMilliseconds(interval);
		_logger.LogDebug("Setting heartbeat to {Interval}", _interval);

		// We cancel the _waitHandle, this should resume the loop in ExecuteAsync.
		_waitHandle.Cancel();
		return ValueTask.CompletedTask;
	}

	/// <inheritdoc cref="IHeartbeatService.Notify" />
	public ValueTask Notify(int? sequence, CancellationToken cancellationToken = default)
	{
		if (_sequence < sequence)
			_sequence = sequence;

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc cref="IHeartbeatService.Acknowledge" />
	public ValueTask Acknowledge(CancellationToken cancellationToken = default)
	{
		_wasAcknowledged = true;

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc cref="IHeartbeatService.Acknowledge" />
	public ValueTask RequestSend(CancellationToken cancellationToken = default)
	{
		// If we don't set acknowledge we'd throw when triggering a heartbeat as we may not have received an ack yet.
		_wasAcknowledged = true;

		_waitHandle.Cancel();
		return ValueTask.CompletedTask;
	}

	/// <inheritdoc cref="BackgroundService.ExecuteAsync" />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (stoppingToken.IsCancellationRequested is false)
		{
			try
			{
				using var linked = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken, _waitHandle.Token);
				await Task.Delay(_interval, linked.Token);
			}
			catch (TaskCanceledException) when (stoppingToken.IsCancellationRequested is false)
			{
				// _waitHandle was cancelled, someone signalled the heartbeat service to continue.
				_waitHandle = new();
			}

			if (_wasAcknowledged is false)
			{
				// TODO: we did NOT receive an ACK!
				_logger.LogWarning("Should send {Sequence}, but did NOT receive an ACK", _sequence);
			}

			await _discordGateway.SendAsync(new HeartbeatEvent
			{
				Sequence = _sequence
			}, stoppingToken);
		}
	}
}