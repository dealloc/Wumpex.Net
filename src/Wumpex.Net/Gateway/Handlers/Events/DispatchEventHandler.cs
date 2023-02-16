using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wumpex.Net.Core.Events.Channels;
using Wumpex.Net.Core.Events.Gateway;
using Wumpex.Net.Core.Events.Guilds;
using Wumpex.Net.Core.Events.Interactions;
using Wumpex.Net.Core.Events.Messages;
using Wumpex.Net.Core.Events.Voice;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Dispatch;
using Wumpex.Net.Gateway.Events.Base;

namespace Wumpex.Net.Gateway.Handlers.Events;

internal class DispatchEventHandler : IDispatchEventHandler
{
	private readonly ILogger<DispatchEventHandler> _logger;
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly HostedWorkerPool _workerPool;
	private readonly IEventSerializer<ReadyEvent> _readySerializer;
	private readonly IEventSerializer<GuildCreateEvent> _guildCreateSerializer;
	private readonly IEventSerializer<GuildDeleteEvent> _guildDeleteSerializer;
	private readonly IEventSerializer<MessageCreateEvent> _messageCreateSerializer;
	private readonly IEventSerializer<MessageDeleteEvent> _messageDeleteSerializer;
	private readonly IEventSerializer<MessageUpdateEvent> _messageUpdateSerializer;
	private readonly IEventSerializer<InteractionCreateEvent> _interactionCreateSerializer;
	private readonly IEventSerializer<ChannelCreateEvent> _channelCreateSerializer;
	private readonly IEventSerializer<ChannelDeleteEvent> _channelDeleteSerializer;
	private readonly IEventSerializer<VoiceStateUpdateEvent> _voiceStateUpdateSerializer;
	private readonly IEventSerializer<VoiceServerUpdateEvent> _voiceServerUpdateSerializer;

	public DispatchEventHandler(ILogger<DispatchEventHandler> logger,
		IServiceScopeFactory serviceScopeFactory,
		HostedWorkerPool workerPool,
		IEventSerializer<ReadyEvent> readySerializer,
		IEventSerializer<GuildCreateEvent> guildCreateSerializer,
		IEventSerializer<GuildDeleteEvent> guildDeleteSerializer,
		IEventSerializer<MessageCreateEvent> messageCreateSerializer,
		IEventSerializer<MessageDeleteEvent> messageDeleteSerializer,
		IEventSerializer<MessageUpdateEvent> messageUpdateSerializer,
		IEventSerializer<InteractionCreateEvent> interactionCreateSerializer,
		IEventSerializer<ChannelCreateEvent> channelCreateSerializer,
		IEventSerializer<ChannelDeleteEvent> channelDeleteSerializer,
		IEventSerializer<VoiceStateUpdateEvent> voiceStateUpdateSerializer,
		IEventSerializer<VoiceServerUpdateEvent> voiceServerUpdateSerializer)
	{
		_logger = logger;
		_serviceScopeFactory = serviceScopeFactory;
		_workerPool = workerPool;
		_readySerializer = readySerializer;
		_guildCreateSerializer = guildCreateSerializer;
		_guildDeleteSerializer = guildDeleteSerializer;
		_messageCreateSerializer = messageCreateSerializer;
		_messageDeleteSerializer = messageDeleteSerializer;
		_messageUpdateSerializer = messageUpdateSerializer;
		_interactionCreateSerializer = interactionCreateSerializer;
		_channelCreateSerializer = channelCreateSerializer;
		_channelDeleteSerializer = channelDeleteSerializer;
		_voiceStateUpdateSerializer = voiceStateUpdateSerializer;
		_voiceServerUpdateSerializer = voiceServerUpdateSerializer;
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
			"MESSAGE_UPDATE" => ScopedDispatch(context, _messageUpdateSerializer.Deserialize(payload), cancellationToken),
			"INTERACTION_CREATE" => ScopedDispatch(context, _interactionCreateSerializer.Deserialize(payload), cancellationToken),
			"CHANNEL_CREATE" => ScopedDispatch(context, _channelCreateSerializer.Deserialize(payload), cancellationToken),
			"CHANNEL_DELETE" => ScopedDispatch(context, _channelDeleteSerializer.Deserialize(payload), cancellationToken),
			"VOICE_STATE_UPDATE" => ScopedDispatch(context, _voiceStateUpdateSerializer.Deserialize(payload), cancellationToken),
			"VOICE_SERVER_UPDATE" => ScopedDispatch(context, _voiceServerUpdateSerializer.Deserialize(payload), cancellationToken),
			_ => throw new NotSupportedException($"Unknown event '{context.EventName}' received")
		};
	}

	/// <summary>
	/// Actually performs the dispatch, wrapping the handler in it's own service scope.
	/// Since this method needs to be async (for the scope lifetime) we need it separate from <see cref="HandleDispatch" />, which takes a <see cref="ReadOnlySpan{T}" />.
	/// </summary>
	private ValueTask ScopedDispatch<TEvent>(EventContext context, TEvent @event, CancellationToken cancellationToken) where TEvent : class
	{
		return _workerPool.QueueDispatch(async (stoppingToken) =>
		{
			await using var scope = _serviceScopeFactory.CreateAsyncScope();
			var heartbeat = scope.ServiceProvider.GetRequiredService<IHeartbeatService>();
			var handlers = scope
				.ServiceProvider
				.GetRequiredService<IEnumerable<IEventHandler<TEvent>>>()
				.ToList();

			if (handlers.Count is 0)
			{
				_logger.LogWarning("No handlers registered for {EventType}", @event.GetType().FullName);

				return;
			}

			await Task.WhenAll(
				handlers.Select(handler => handler.HandleEvent(@event, context, stoppingToken).AsTask())
			);

			await heartbeat.Notify(context.Sequence, stoppingToken);
		}, context, cancellationToken);
	}
}