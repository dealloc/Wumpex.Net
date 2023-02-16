namespace Wumpex.Net.Voice.Contracts;

public interface IVoiceService
{
	ValueTask ConnectAsync(string guild, string channel, CancellationToken cancellationToken = default);
}