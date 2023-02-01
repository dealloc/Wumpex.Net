using Elixus.Discord.Core.Constants.Gateway;

namespace Elixus.Discord.Core.Attributes;

/// <summary>
/// Indicates which <see cref="GatewayIntents" /> are required for this event.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class IntentAttribute : Attribute
{
	/// <summary>
	/// The <see cref="GatewayIntents" /> this event requires to be fired.
	/// </summary>
	public GatewayIntents Intent { get; }

	/// <summary>
	/// Creates a new instance of <see cref="IntentAttribute" />.
	/// </summary>
	public IntentAttribute(GatewayIntents intent)
	{
		Intent = intent;
	}
}