using Elixus.Discord.Gateway.Contracts.Events;
using Elixus.Discord.Gateway.Events.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Elixus.Discord.Gateway.Events.Handlers;

internal class DispatchEventHandler : IDispatchEventHandler
{
	/// <inheritdoc cref="IDispatchEventHandler.HandleDispatch" />
	public ValueTask HandleDispatch(IServiceScope scope, EventContext context, ref ReadOnlySpan<byte> payload, CancellationToken cancellationToken)
	{
		return ValueTask.CompletedTask;
	}
}