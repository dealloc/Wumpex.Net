using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wumpex.Net.Core.Events.Gateway;
using Wumpex.Net.Core.Events.Guilds;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Events.Messages;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events;

internal class DispatchEventHandler : IDispatchEventHandler
{
	private readonly ILogger<DispatchEventHandler> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly IEventSerializer<ReadyEvent> _readySerializer;
	private readonly IEventSerializer<GuildCreateEvent> _guildCreateSerializer;
	private readonly IEventSerializer<GuildDeleteEvent> _guildDeleteSerializer;
	private readonly IEventSerializer<MessageCreateEvent> _messageCreateSerializer;
	private readonly IEventSerializer<MessageDeleteEvent> _messageDeleteSerializer;
	private readonly IEventSerializer<InteractionCreateEvent> _interactionCreateSerializer;

	public DispatchEventHandler(ILogger<DispatchEventHandler> logger,
		IServiceScopeFactory serviceScopeFactory,
		IEventSerializer<ReadyEvent> readySerializer,
		IEventSerializer<GuildCreateEvent> guildCreateSerializer,
		IEventSerializer<GuildDeleteEvent> guildDeleteSerializer,
		IEventSerializer<MessageCreateEvent> messageCreateSerializer,
		IEventSerializer<MessageDeleteEvent> messageDeleteSerializer,
		IEventSerializer<InteractionCreateEvent> interactionCreateSerializer)
	{
		_logger = logger;
		_serviceScopeFactory = serviceScopeFactory;
		_readySerializer = readySerializer;
		_guildCreateSerializer = guildCreateSerializer;
		_guildDeleteSerializer = guildDeleteSerializer;
		_messageCreateSerializer = messageCreateSerializer;
		_messageDeleteSerializer = messageDeleteSerializer;
		_interactionCreateSerializer = interactionCreateSerializer;
	}

	/// <inheritdoc cref="IDispatchEventHandler.HandleDispatch" />
	public ValueTask HandleDispatch(EventContext context, ref ReadOnlySpan<byte> payload, CancellationToken cancellationToken)
	{
		return context.EventName?.ToUpperInvariant() switch
		{
			"READY" => ScopedDispatch(context, _readySerializer.Deserialize(payload), cancellationToken),
			"GUILD_CREATE" => ScopedDispatch(context, _guildCreateSerializer.Deserialize(payload), cancellationToken),
			"GUILD_DELETE" => ScopedDispatch(context, _guildDeleteSerializer.Deserialize(payload), cancellationToken),
			"MESSAGE_CREATE" => ScopedDispatch(context, _messageCreateSerializer.Deserialize(payload), cancellationToken),
			"MESSAGE_DELETE" => ScopedDispatch(context, _messageDeleteSerializer.Deserialize(payload), cancellationToken),
			"INTERACTION_CREATE" => ScopedDispatch(context, _interactionCreateSerializer.Deserialize(payload), cancellationToken),
			_ => throw new NotSupportedException($"Unknown event '{context.EventName}' received")
		};
	}

	/// <summary>
	/// Actually performs the dispatch, wrapping the handler in it's own service scope.
	/// Since this method needs to be async (for the scope lifetime) we need it separate from <see cref="HandleDispatch" />, which takes a <see cref="ReadOnlySpan{T}" />.
	/// </summary>
	private async ValueTask ScopedDispatch<TEvent>(EventContext context, TEvent @event, CancellationToken cancellationToken) where TEvent : class
	{
		await using var scope = _serviceScopeFactory.CreateAsyncScope();
		var handler = scope.ServiceProvider.GetService<IEventHandler<TEvent>>();

		if (handler is null)
		{
			_logger.LogWarning("No handlers registered for {EventType}", @event.GetType().FullName);

			return;
		}

		await handler.HandleEvent(@event, context, cancellationToken);
	}
}