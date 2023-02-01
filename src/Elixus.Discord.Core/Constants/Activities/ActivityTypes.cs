namespace Elixus.Discord.Core.Constants.Activities;

/// <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-types" />
public enum ActivityTypes
{
	/// <summary>
	/// Playing {name}
	/// </summary>
	Game = 0,

	/// <summary>
	/// Streaming {details}
	/// </summary>
	Streaming = 1,

	/// <summary>
	/// Listening to {name}
	/// </summary>
	Listening = 2,

	/// <summary>
	/// Watching {name}
	/// </summary>
	Watching = 3,

	/// <summary>
	/// {emoji} {name}
	/// </summary>
	Custom = 4,

	/// <summary>
	/// Competing in {name}
	/// </summary>
	Competing = 5
}