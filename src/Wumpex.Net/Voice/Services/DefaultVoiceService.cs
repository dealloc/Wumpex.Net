using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Wumpex.Net.Core.Contracts;
using Wumpex.Net.Core.Events.Voice;
using Wumpex.Net.Gateway.Contracts;
using Wumpex.Net.Gateway.Contracts.Events;
using Wumpex.Net.Gateway.Events.Base;
using Wumpex.Net.Voice.Contracts;

namespace Wumpex.Net.Voice.Services;

/// <summary>
/// Default implementation of the <see cref="IVoiceService" />.
/// </summary>
public class DefaultVoiceService : IVoiceService
{
	private readonly ILogger<DefaultVoiceService> _logger;
	private readonly IWumpex _wumpex;
	private readonly IDiscordGateway _gateway;
	private readonly IDeferredEventListener<VoiceStateUpdateEvent> _stateUpdateListener;
	private readonly IDeferredEventListener<VoiceServerUpdateEvent> _serverUpdateListener;

	public DefaultVoiceService(ILogger<DefaultVoiceService> logger,
		IWumpex wumpex,
		IDiscordGateway gateway,
		IDeferredEventListener<VoiceStateUpdateEvent> stateUpdateListener,
		IDeferredEventListener<VoiceServerUpdateEvent> serverUpdateListener)
	{
		_logger = logger;
		_wumpex = wumpex;
		_gateway = gateway;
		_stateUpdateListener = stateUpdateListener;
		_serverUpdateListener = serverUpdateListener;
	}

	/// <inheritdoc cref="IVoiceService.ConnectAsync" />
	public async ValueTask ConnectAsync(string guild, string channel, CancellationToken cancellationToken = default)
	{
		var user = await _wumpex.User;
		var stateTask = _stateUpdateListener.WaitFor(@event => string.Equals(@event.UserId, user.Id), TimeSpan.FromSeconds(5), cancellationToken);
		var serverTask = _serverUpdateListener.WaitFor(@event => string.Equals(@event.GuildId, guild), TimeSpan.FromSeconds(5), cancellationToken);

		await _gateway.SendAsync(new VoiceStateUpdateEvent
		{
			GuildId = guild,
			ChannelId = channel,
			SelfMute = true,
			SelfDeaf = true
		}, cancellationToken);

		var state = await stateTask;
		var server = await serverTask;
	}
}