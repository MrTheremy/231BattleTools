using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Cosmos;

namespace KingshotTools.Data.Documents;

/// <summary>Base class for every document partitioned by kingdom (<c>/mapKid</c>).</summary>
public abstract class GameDocument
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Kingdom (map) id; the first (or only) partition key level.</summary>
    [JsonPropertyName("mapKid")]
    public required int MapKid { get; init; }

    /// <summary>Discriminator; see <see cref="DocumentTypes"/>.</summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Raw game object as decoded JSON, stored unmodified.</summary>
    [JsonPropertyName("data")]
    public JsonElement? Data { get; set; }

    /// <summary>See <see cref="DocumentStatus"/>.</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = DocumentStatus.Active;

    /// <summary>Per-item time to live in seconds; <c>null</c> uses the container default.</summary>
    [JsonPropertyName("ttl")]
    public int? Ttl { get; set; }

    /// <summary>Partition key value matching the container's partition key paths.</summary>
    public virtual PartitionKey GetPartitionKey() => new(MapKid);
}

/// <summary>Base class for documents partitioned by kingdom and UTC day (<c>/mapKid</c>, <c>/day</c>).</summary>
public abstract class DailyGameDocument : GameDocument
{
    /// <summary>UTC day as <c>yyyy-MM-dd</c>; the second partition key level.</summary>
    [JsonPropertyName("day")]
    public required string Day { get; init; }

    public override PartitionKey GetPartitionKey() =>
        new PartitionKeyBuilder().Add(MapKid).Add(Day).Build();
}
