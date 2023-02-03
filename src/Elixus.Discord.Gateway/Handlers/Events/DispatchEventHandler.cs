using Elixus.Discord.Core.Events.Gateway;
using Elixus.Discord.Core.Events.Guilds;
using Elixus.Discord.Core.Events.Messages;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Elixus.Discord.Gateway.Handlers.Events;

internal class DispatchEventHandler : IDispatchEventHandler
{
	private readonly ILogger<DispatchEventHandler> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly IEventSerializer<ReadyEvent> _readySerializer;
	private readonly IEventSerializer<GuildCreateEvent> _guildCreateSerializer;
	private readonly IEventSerializer<MessageCreateEvent> _messageCreateSerializer;

	public DispatchEventHandler(ILogger<DispatchEventHandler> logger,
		IServiceScopeFactory serviceScopeFactory,
		IEventSerializer<ReadyEvent> readySerializer,
		IEventSerializer<GuildCreateEvent> guildCreateSerializer,
		IEventSerializer<MessageCreateEvent> messageCreateSerializer)
	{
		_logger = logger;
		_serviceScopeFactory = serviceScopeFactory;
		_readySerializer = readySerializer;
		_guildCreateSerializer = guildCreateSerializer;
		_messageCreateSerializer = messageCreateSerializer;
	}

	/// <inheritdoc cref="IDispatchEventHandler.HandleDispatch" />
	public ValueTask HandleDispatch(EventContext context, ref ReadOnlySpan<byte> payload, CancellationToken cancellationToken)
	{
		return context.EventName?.ToUpperInvariant() switch
		{
			"READY" => ScopedDispatch(context, _readySerializer.Deserialize(payload), cancellationToken),
			"GUILD_CREATE" => ScopedDispatch(context, _guildCreateSerializer.Deserialize(payload), cancellationToken),
			"MESSAGE_CREATE" => ScopedDispatch(context, _messageCreateSerializer.Deserialize(payload), cancellationToken),
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