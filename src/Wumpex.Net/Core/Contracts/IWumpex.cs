using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Contracts;

/// <summary>
/// Provides contextual information about the Wumpex runtime.
/// </summary>
public interface IWumpex
{
	/// <summary>
	/// Gets the current <see cref="Wumpex.Net.Core.Models.Users.User" />.
	/// </summary>
	public Task<User> User { get; set; }
}