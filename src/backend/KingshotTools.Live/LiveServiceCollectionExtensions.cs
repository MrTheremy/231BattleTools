using KingshotTools.Live.Broadcast;
using KingshotTools.Live.ChangeFeed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KingshotTools.Live;

public static class LiveServiceCollectionExtensions
{
    /// <summary>Registers SignalR, the broadcaster and the change feed processor.</summary>
    public static IServiceCollection AddKingshotLive(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<LiveOptions>().Bind(configuration.GetSection(LiveOptions.SectionName));
        services.AddSignalR();
        services.AddSingleton<ILiveBroadcaster, SignalRLiveBroadcaster>();
        services.AddHostedService<WorldStateChangeFeedService>();
        return services;
    }
}
