using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Events.Handlers;

internal class DispatchEventHandler : IDispatchEventHandler
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public DispatchEventHandler(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	/// <inheritdoc cref="IDispatchEventHandler.HandleDispatch" />
	public ValueTask HandleDispatch(EventContext context, ref ReadOnlySpan<byte> payload, CancellationToken cancellationToken)
	{
		return ValueTask.CompletedTask;
	}

	/// <summary>
	/// Actually performs the dispatch, wrapping the handler in it's own service scope.
	/// Since this method needs to be async (for the scope lifetime) we need it separate from <see cref="HandleDispatch" />, which takes a <see cref="ReadOnlySpan{T}" />.
	/// </summary>
	private async ValueTask ScopedDispatch<TEvent>(EventContext context, TEvent @event, CancellationToken cancellationToken) where TEvent : class
	{
		await using var scope = _serviceScopeFactory.CreateAsyncScope();
		var handler = scope.ServiceProvider.GetRequiredService<IEventHandler<TEvent>>();

		await handler.HandleEvent(@event, context, cancellationToken);
	}
}