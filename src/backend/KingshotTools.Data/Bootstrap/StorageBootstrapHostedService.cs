using KingshotTools.Data.Blob;
using KingshotTools.Data.Cosmos;
using KingshotTools.Data.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingshotTools.Data.Bootstrap;

/// <summary>
/// Development-only startup step that creates the Cosmos containers and the raw-frames blob container.
/// The API host registers it only in the Development environment; the options flags can switch each part off.
/// </summary>
public sealed class StorageBootstrapHostedService(
    ICosmosBootstrapper cosmos,
    IRawFrameArchive archive,
    IOptions<CosmosOptions> cosmosOptions,
    IOptions<BlobOptions> blobOptions,
    ILogger<StorageBootstrapHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (cosmosOptions.Value.EnsureCreatedOnStartup)
        {
            logger.LogInformation("Creating Cosmos database and containers (Development only)");
            await cosmos.EnsureCreatedAsync(cancellationToken);
        }

        if (blobOptions.Value.EnsureCreatedOnStartup)
        {
            logger.LogInformation("Creating blob containers (Development only)");
            await archive.EnsureCreatedAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
