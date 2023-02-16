using Wumpex.Net.Core.Contracts;
using Wumpex.Net.Core.Models.Users;

namespace Wumpex.Net.Core.Services;

/// <summary>
/// Default implementation of the <see cref="IWumpex" /> contract.
/// </summary>
public class DefaultWumpex : IWumpex
{
	/// <inheritdoc cref="IWumpex.User" />
	public Task<User> User { get; set; } = Task.FromException<User>(new TaskCanceledException("User not configured yet"));
}