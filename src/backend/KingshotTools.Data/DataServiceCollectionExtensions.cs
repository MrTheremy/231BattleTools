using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs;
using KingshotTools.Data.Blob;
using KingshotTools.Data.Cosmos;
using KingshotTools.Data.Health;
using KingshotTools.Data.Options;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KingshotTools.Data;

public static class DataServiceCollectionExtensions
{
    /// <summary>Registers Cosmos and Blob clients, stores and health checks.</summary>
    public static IServiceCollection AddKingshotData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<CosmosOptions>()
            .Bind(configuration.GetSection(CosmosOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.ConnectionString), "Cosmos:ConnectionString is required.")
            .ValidateOnStart();

        services.AddOptions<BlobOptions>()
            .Bind(configuration.GetSection(BlobOptions.SectionName))
            .Validate(o => !string.IsNullOrWhiteSpace(o.ConnectionString), "Blob:ConnectionString is required.")
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<CosmosOptions>>().Value;
            var clientOptions = new CosmosClientOptions
            {
                Serializer = new SystemTextJsonCosmosSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }),
            };

            if (options.UseEmulatorConnectionMode)
            {
                clientOptions.ConnectionMode = ConnectionMode.Gateway;
                clientOptions.LimitToEndpoint = true;
            }

            return new CosmosClient(options.ConnectionString, clientOptions);
        });

        services.AddSingleton(sp => new BlobServiceClient(sp.GetRequiredService<IOptions<BlobOptions>>().Value.ConnectionString));

        services.AddSingleton<ICosmosBootstrapper, CosmosBootstrapper>();
        services.AddSingleton<IGameDocumentStore, CosmosGameDocumentStore>();
        services.AddSingleton<IRawFrameArchive, BlobRawFrameArchive>();

        services.AddHealthChecks()
            .AddCheck<CosmosHealthCheck>("cosmos", tags: ["ready"])
            .AddCheck<BlobHealthCheck>("blob", tags: ["ready"]);

        return services;
    }
}
