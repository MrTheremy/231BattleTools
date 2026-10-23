using KingshotTools.Data.Cosmos;

namespace KingshotTools.Data.Tests;

public class CosmosContainerCatalogTests
{
    [Fact]
    public void Defines_every_expected_container() =>
        Assert.Equal(
            ["messages", "worldState", "players", "battles", "snapshots", "leases"],
            CosmosContainerCatalog.All.Select(c => c.Name));

    [Fact]
    public void Messages_use_hierarchical_partition_key() =>
        Assert.Equal(["/mapKid", "/day"], Spec(ContainerNames.Messages).PartitionKeyPaths);

    [Fact]
    public void World_state_is_partitioned_by_kingdom() =>
        Assert.Equal(["/mapKid"], Spec(ContainerNames.WorldState).PartitionKeyPaths);

    [Fact]
    public void Leases_are_partitioned_by_id_as_the_change_feed_requires() =>
        Assert.Equal(["/id"], Spec(ContainerNames.Leases).PartitionKeyPaths);

    private static ContainerSpec Spec(string name) => CosmosContainerCatalog.All.Single(c => c.Name == name);
}
