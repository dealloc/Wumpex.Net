using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Services;

/// <summary>
/// Default implementation of <see cref="IDeferredEventListener{TEvent}" /> that uses a <see cref="ConcurrentDictionary{TKey,TValue}" /> under the hood.
/// </summary>
public sealed class DefaultDeferredEventListener<TEvent> : IDeferredEventListener<TEvent> where TEvent : class
{
	private readonly ILogger<DefaultDeferredEventListener<TEvent>> _logger;
	private readonly ConcurrentDictionary<Func<TEvent, bool>, TaskCompletionSource<TEvent>> _listeners = new();

	public DefaultDeferredEventListener(ILogger<DefaultDeferredEventListener<TEvent>> logger)
	{
		_logger = logger;
	}

	/// <inheritdoc cref="IEventHandler{TEvent}.HandleEvent" />
	public ValueTask HandleEvent(TEvent @event, EventContext context, CancellationToken cancellationToken)
	{
		// Get all keys upfront since modifications while iterating are a gray zone thread-safety wise.
		var predicates = _listeners.Keys.ToList();

		foreach (var predicate in predicates)
		{
			if (predicate(@event) && _listeners.TryGetValue(predicate, out var source))
			{
				try
				{
					source.SetResult(@event);
				}
				catch (Exception exception)
				{
					_logger.LogTrace(exception, "An error occured while trying to set the result of a deferred event");
				}
			}
		}

		return ValueTask.CompletedTask;
	}

	/// <inheritdoc cref="IDeferredEventListener{TEvent}.WaitFor" />
	public async Task<TEvent> WaitFor(Func<TEvent, bool> predicate, TimeSpan timeout, CancellationToken cancellationToken = default)
	{
		var token = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		token.CancelAfter(timeout);

		try
		{
			var source = new TaskCompletionSource<TEvent>();
			_listeners.TryAdd(predicate, source);

			return await source.Task.WaitAsync(token.Token);
		}
		finally
		{
			_listeners.TryRemove(predicate, out _);
		}
	}
}