using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Wumpex.Net.Gateway.Constants;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events;
using Wumpex.Net.Gateway.Events.Base;
using Wumpex.Net.Gateway.Serialization.Contexts;

namespace Wumpex.Net.Gateway.Serialization;

/// <summary>
/// Serializes the <see cref="ResumeEvent" /> for receiving and sending from the gateway.
/// </summary>
public class ResumeEventSerializer : IEventSerializer<ResumeEvent>
{
	private readonly JsonTypeInfo<GatewayEvent<ResumeEvent>> _gatewayType = GatewayEventSerializerContext.Default.GatewayEventResumeEvent;

	/// <inheritdoc cref="IEventSerializer{TEvent}.Serialize" />
	public ArraySegment<byte> Serialize(ResumeEvent @event)
		=> JsonSerializer.SerializeToUtf8Bytes(new GatewayEvent<ResumeEvent>
		{
			Opcode = GatewayOpcodes.Identify,
			Event = @event
		}, _gatewayType);

	/// <inheritdoc cref="IEventSerializer{TEvent}.Deserialize" />
	public ResumeEvent Deserialize(ReadOnlySpan<byte> payload)
		=> throw new NotSupportedException("Resume does not support receiving");
}