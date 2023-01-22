namespace Elixus.Discord.Api.Exceptions;

/// <summary>
/// Thrown when the API returns a response that wasn't expected by the client.
/// </summary>
public sealed class UnexpectedResponseException : Exception
{
	/// <summary>
	/// The endpoint that returned the unexpected response.
	/// </summary>
	public string Endpoint { get; }

	/// <summary>
	/// Creates a new instance of <see cref="UnexpectedResponseException" />.
	/// </summary>
	public UnexpectedResponseException(string endpoint, string message) : base(message)
	{
		Endpoint = endpoint;
	}

	/// <summary>
	/// Creates a new instance of <see cref="UnexpectedResponseException" /> with the default error message.
	/// </summary>
	public UnexpectedResponseException(string endpoint) : this(endpoint, "Unexpected response received from the API")
	{
		//
	}
}
