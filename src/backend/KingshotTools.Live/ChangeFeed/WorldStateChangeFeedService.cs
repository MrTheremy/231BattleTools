using System.Text.Json;
using KingshotTools.Data.Cosmos;
using KingshotTools.Data.Options;
using KingshotTools.Live.Broadcast;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingshotTools.Live.ChangeFeed;

/// <summary>
/// Hosts the Cosmos change feed processor on <c>worldState</c> and forwards changes to <see cref="ILiveBroadcaster"/>.
/// Deletes are not reported by the change feed, so expiring documents are first upserted with
/// <c>status: "ended"</c> (docs/adr/0005).
/// </summary>
public sealed class WorldStateChangeFeedService(
    CosmosClient client,
    IOptions<CosmosOptions> cosmosOptions,
    IOptions<LiveOptions> liveOptions,
    ILiveBroadcaster broadcaster,
    ILogger<WorldStateChangeFeedService> logger) : IHostedService
{
    private ChangeFeedProcessor? _processor;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var live = liveOptions.Value;
        if (!live.ChangeFeedEnabled)
        {
            logger.LogInformation("World-state change feed is disabled");
            return;
        }

        var database = client.GetDatabase(cosmosOptions.Value.DatabaseName);
        _processor = database.GetContainer(ContainerNames.WorldState)
            .GetChangeFeedProcessorBuilder<JsonElement>(live.ProcessorName, HandleChangesAsync)
            .WithInstanceName(live.InstanceName ?? Environment.MachineName)
            .WithLeaseContainer(database.GetContainer(ContainerNames.Leases))
            .Build();

        await _processor.StartAsync();
        logger.LogInformation("World-state change feed processor {Processor} started", live.ProcessorName);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_processor is not null)
        {
            await _processor.StopAsync();
        }
    }

    private Task HandleChangesAsync(
        ChangeFeedProcessorContext context,
        IReadOnlyCollection<JsonElement> changes,
        CancellationToken cancellationToken)
    {
        logger.LogDebug("Received {ChangeCount} world-state changes from lease {LeaseToken}", changes.Count, context.LeaseToken);
        _ = broadcaster;

        // TODO: group changes by mapKid and call broadcaster.BroadcastWorldStateAsync per kingdom.
        return Task.CompletedTask;
    }
}
