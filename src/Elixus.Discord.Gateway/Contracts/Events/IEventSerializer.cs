namespace Elixus.Discord.Gateway.Contracts.Events;

/// <summary>
/// Handles serialization of gateway events.
/// </summary>
public interface IEventSerializer<TEvent> where TEvent : class, new()
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