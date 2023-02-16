using Microsoft.Extensions.DependencyInjection;
using Wumpex.Net.Core.Events.Voice;
using Wumpex.Net.Gateway.Extensions;
using Wumpex.Net.Voice.Contracts;
using Wumpex.Net.Voice.Services;

namespace Wumpex.Net.Voice.Extensions;

/// <summary>
/// Contains extension methods for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Adds Discord Voice related services.
	/// </summary>
	/// <param name="services"></param>
	public static void AddWumpexVoice(this IServiceCollection services)
	{
		services.AddSingleton<IVoiceService, DefaultVoiceService>();
		services.AddDeferredEventListener<VoiceStateUpdateEvent>();
		services.AddDeferredEventListener<VoiceServerUpdateEvent>();
	}
}