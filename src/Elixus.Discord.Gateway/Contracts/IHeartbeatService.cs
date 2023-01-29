namespace Elixus.Discord.Gateway.Contracts;

/// <summary>
/// Allows sending heartbeats and getting the latest heartbeat status.
/// </summary>
public interface IHeartbeatService
{
	/// <summary>
	/// Gets the last received sequence.
	/// </summary>
	ValueTask<int?> Sequence { get; }

	/// <summary>
	/// Tells the <see cref="IHeartbeatService" /> to start sending out heartbeats at the given <paramref name="interval" /> (in milliseconds).
	/// </summary>
	/// <remarks>
	/// This method should be omnipotent - it can be called multiple times and should support this.
	/// </remarks>
	ValueTask Start(int interval, CancellationToken cancellationToken = default);

	/// <summary>
	/// Notifies the <see cref="IHeartbeatService" /> that a new sequence has been received from the gateway to send in the next heartbeat.
	/// </summary>
	ValueTask Notify(int? sequence, CancellationToken cancellationToken = default);

	/// <summary>
	/// Notifies the <see cref="IHeartbeatService" /> that a heartbeat acknowledge has been received.
	/// </summary>
	/// <remarks>
	/// If <see cref="Acknowledge" /> isn't called between heartbeats the gateway will be requested to close and resume.
	/// </remarks>
	ValueTask Acknowledge(CancellationToken cancellationToken = default);

	/// <summary>
	/// Requests that the heartbeat service sends a heartbeat as soon as reasonably possible.
	/// </summary>
	ValueTask RequestSend(CancellationToken cancellationToken = default);
}