namespace Wumpex.Net.Core.Constants.Channels;

/// <see href="https://discord.com/developers/docs/resources/channel#message-object-message-activity-types" />
public enum MessageActivityTypes
{
	/// <summary>
	/// JOIN
	/// </summary>
	Join = 1,

	/// <summary>
	/// SPECTATE
	/// </summary>
	Spectate = 2,

	/// <summary>
	/// LISTEN
	/// </summary>
	Listen = 3,

	/// <summary>
	/// JOIN_REQUEST
	/// </summary>
	JoinRequest = 5
}