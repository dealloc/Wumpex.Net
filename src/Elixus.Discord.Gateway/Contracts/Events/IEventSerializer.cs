using Elixus.Discord.Gateway.Events.Base;

namespace Elixus.Discord.Gateway.Contracts.Events;

/// <summary>
/// Handles serialization of gateway events.
/// </summary>
internal interface IEventSerializer<TEvent> where TEvent : GatewayEvent
{
	/// <summary>
	/// Serializes <typeparamref name="TEvent" /> into a binary form.
	/// </summary>
	ArraySegment<byte> Serialize(TEvent @event);

	/// <summary>
	/// Deserialize <typeparamref name="TEvent" /> from a binary form.
	/// </summary>
	TEvent Deserialize(ReadOnlySpan<byte> payload);
}