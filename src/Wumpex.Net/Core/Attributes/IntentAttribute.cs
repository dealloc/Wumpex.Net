using Wumpex.Net.Core.Constants.Gateway;

namespace Wumpex.Net.Core.Attributes;

/// <summary>
/// Indicates which <see cref="GatewayIntents" /> are required for this event.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
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