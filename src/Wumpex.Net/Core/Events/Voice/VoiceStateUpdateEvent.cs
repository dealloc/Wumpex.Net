using Wumpex.Net.Core.Attributes;
using Wumpex.Net.Core.Constants.Gateway;
using Wumpex.Net.Core.Models.Voice;

namespace Wumpex.Net.Core.Events.Voice;

/// <see href="https://discord.com/developers/docs/topics/voice-connections#retrieving-voice-server-information" />
[Intent(GatewayIntents.GuildVoiceStates)]
public sealed class VoiceStateUpdateEvent : VoiceState
{
	//
}