using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Contracts.Events;

internal interface IEventSerializer<TEvent> where TEvent : GatewayEvent
{
	ArraySegment<byte> Serialize(TEvent @event);

	TEvent Deserialize(ReadOnlySpan<byte> payload);
}