using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using KingshotTools.Data.Documents;

namespace KingshotTools.Data.Tests;

public class GameDocumentSerializationTests
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    [Fact]
    public void Message_document_writes_partition_key_fields_and_raw_data()
    {
        var document = new MessageDocument
        {
            Id = "m-1",
            MapKid = 231,
            Day = "2026-10-23",
            Type = DocumentTypes.Message,
            Data = JsonDocument.Parse("""{"foo":1}""").RootElement,
            Ttl = 3600,
        };

        var json = JsonNode.Parse(JsonSerializer.Serialize(document, Options))!.AsObject();

        Assert.Equal("m-1", (string?)json["id"]);
        Assert.Equal(231, (int?)json["mapKid"]);
        Assert.Equal("2026-10-23", (string?)json["day"]);
        Assert.Equal("message", (string?)json["type"]);
        Assert.Equal(1, (int?)json["data"]!["foo"]);
        Assert.Equal("active", (string?)json["status"]);
        Assert.Equal(3600, (int?)json["ttl"]);
    }

    [Fact]
    public void Null_ttl_is_omitted_so_the_container_default_applies()
    {
        var document = new WorldStateDocument { Id = DocumentIds.MapObject(1, 2), MapKid = 231, Type = DocumentTypes.MapObject };

        var json = JsonNode.Parse(JsonSerializer.Serialize(document, Options))!.AsObject();

        Assert.False(json.ContainsKey("ttl"));
    }
}
