using Elixus.Discord.Gateway.Constants;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Contracts.Events;

/// <summary>
/// Specialized <see cref="IEventHandler{TEvent}" /> for <see cref="GatewayOpcodes.Dispatch" />.
/// Since the dispatch event can contain a wide variety of events the parsing of it's payload is delegated to a specialized
/// handler so we can keep the <see cref="IDiscordGateway" /> clean.
/// </summary>
internal interface IDispatchEventHandler
{
	/// <summary>
	/// Infer the event type sent in the dispatch event and hand it over to the appropriate handlers.
	/// </summary>
	ValueTask HandleDispatch(EventContext context, ref ReadOnlySpan<byte> payload, CancellationToken cancellationToken);
}