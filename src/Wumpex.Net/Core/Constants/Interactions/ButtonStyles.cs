namespace Wumpex.Net.Core.Constants.Interactions;

/// <see href="https://discord.com/developers/docs/interactions/message-components#button-object-button-styles" />
public enum ButtonStyles
{
	/// <summary>
	/// Blurple.
	/// </summary>
	/// <remarks>
	/// Requires custom_id.
	/// </remarks>
	Primary = 1,

	/// <summary>
	/// Grey.
	/// </summary>
	/// <remarks>
	/// Requires custom_id.
	/// </remarks>
	Secondary = 2,

	/// <summary>
	/// Green.
	/// </summary>
	/// <remarks>
	/// Requires custom_id.
	/// </remarks>
	Success = 3,

	/// <summary>
	/// Red.
	/// </summary>
	/// <remarks>
	/// Requires custom_id.
	/// </remarks>
	Danger = 4,

	/// <summary>
	/// Grey, navigates to a URL.
	/// </summary>
	/// <remarks>
	/// Requires url.
	/// </remarks>
	Link = 5
}