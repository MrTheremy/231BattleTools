using KingshotTools.Data.Documents;
using KingshotTools.Data.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace KingshotTools.Data.Cosmos;

public sealed class CosmosGameDocumentStore(CosmosClient client, IOptions<CosmosOptions> options) : IGameDocumentStore
{
    public Task UpsertAsync<TDocument>(string containerName, TDocument document, CancellationToken cancellationToken)
        where TDocument : GameDocument
    {
        ArgumentNullException.ThrowIfNull(document);

        // TODO: batching, retries on 429 and ETag-based concurrency once the ingest worker writes for real.
        var container = client.GetContainer(options.Value.DatabaseName, containerName);
        return container.UpsertItemAsync(document, document.GetPartitionKey(), cancellationToken: cancellationToken);
    }
}
