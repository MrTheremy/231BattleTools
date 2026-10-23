using KingshotTools.Data.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingshotTools.Data.Cosmos;

public sealed class CosmosBootstrapper(
    CosmosClient client,
    IOptions<CosmosOptions> options,
    ILogger<CosmosBootstrapper> logger) : ICosmosBootstrapper
{
    public async Task EnsureCreatedAsync(CancellationToken cancellationToken)
    {
        var databaseName = options.Value.DatabaseName;
        var database = (await client.CreateDatabaseIfNotExistsAsync(databaseName, cancellationToken: cancellationToken)).Database;

        foreach (var spec in CosmosContainerCatalog.All)
        {
            var properties = new ContainerProperties(spec.Name, [.. spec.PartitionKeyPaths])
            {
                DefaultTimeToLive = spec.DefaultTimeToLive,
            };

            var response = await database.CreateContainerIfNotExistsAsync(properties, cancellationToken: cancellationToken);
            logger.LogInformation(
                "Cosmos container {Database}/{Container} ready ({Status}), partition key {PartitionKey}",
                databaseName,
                spec.Name,
                response.StatusCode,
                string.Join(", ", spec.PartitionKeyPaths));
        }
    }
}
