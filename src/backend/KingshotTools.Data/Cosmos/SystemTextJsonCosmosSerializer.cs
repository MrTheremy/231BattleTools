using System.Text.Json;
using Microsoft.Azure.Cosmos;

namespace KingshotTools.Data.Cosmos;

/// <summary>Makes the Cosmos SDK use System.Text.Json so document models only need STJ attributes.</summary>
public sealed class SystemTextJsonCosmosSerializer(JsonSerializerOptions options) : CosmosSerializer
{
    public override T FromStream<T>(Stream stream)
    {
        using (stream)
        {
            if (typeof(Stream).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)stream;
            }

            return JsonSerializer.Deserialize<T>(stream, options)!;
        }
    }

    public override Stream ToStream<T>(T input)
    {
        var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, input, options);
        stream.Position = 0;
        return stream;
    }
}
