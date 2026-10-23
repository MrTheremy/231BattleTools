using KingshotTools.Data.Documents;

namespace KingshotTools.Data.Cosmos;

/// <summary>Writes game documents to Cosmos DB.</summary>
public interface IGameDocumentStore
{
    Task UpsertAsync<TDocument>(string containerName, TDocument document, CancellationToken cancellationToken)
        where TDocument : GameDocument;
}
