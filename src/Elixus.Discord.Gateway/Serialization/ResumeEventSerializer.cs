using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events;
using Elixus.Discord.Gateway.Events.Base;
using Elixus.Discord.Gateway.Serialization.Contexts;

namespace Elixus.Discord.Gateway.Serialization;

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