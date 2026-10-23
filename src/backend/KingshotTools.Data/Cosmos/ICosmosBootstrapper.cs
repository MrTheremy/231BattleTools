namespace KingshotTools.Data.Cosmos;

/// <summary>Creates the database and containers from <see cref="CosmosContainerCatalog"/>.</summary>
public interface ICosmosBootstrapper
{
    Task EnsureCreatedAsync(CancellationToken cancellationToken);
}
