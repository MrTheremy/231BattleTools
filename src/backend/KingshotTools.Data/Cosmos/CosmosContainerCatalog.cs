namespace KingshotTools.Data.Cosmos;

/// <summary>Definition of one Cosmos container.</summary>
/// <param name="Name">Container id.</param>
/// <param name="PartitionKeyPaths">One path, or several for a hierarchical partition key.</param>
/// <param name="DefaultTimeToLive">Container TTL in seconds; -1 enables per-item <c>ttl</c> without a default, <c>null</c> disables TTL.</param>
public sealed record ContainerSpec(string Name, IReadOnlyList<string> PartitionKeyPaths, int? DefaultTimeToLive);

/// <summary>All containers the backend expects to exist.</summary>
public static class CosmosContainerCatalog
{
    public static IReadOnlyList<ContainerSpec> All { get; } =
    [
        new(ContainerNames.Messages, ["/mapKid", "/day"], DefaultTimeToLive: -1),
        new(ContainerNames.WorldState, ["/mapKid"], DefaultTimeToLive: -1),
        new(ContainerNames.Players, ["/mapKid"], DefaultTimeToLive: null),
        new(ContainerNames.Battles, ["/mapKid"], DefaultTimeToLive: null),
        new(ContainerNames.Snapshots, ["/mapKid"], DefaultTimeToLive: -1),
        new(ContainerNames.Leases, ["/id"], DefaultTimeToLive: null),
    ];
}
