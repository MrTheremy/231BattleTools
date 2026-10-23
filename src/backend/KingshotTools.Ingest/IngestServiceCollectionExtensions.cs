using KingshotTools.Decoder;
using KingshotTools.Decoder.Fpnn;
using KingshotTools.Decoder.Schema;
using KingshotTools.Decoder.Sproto;
using KingshotTools.Ingest.Queue;
using KingshotTools.Ingest.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KingshotTools.Ingest;

public static class IngestServiceCollectionExtensions
{
    /// <summary>Registers the ingest queue, the decoder and the decoder worker.</summary>
    public static IServiceCollection AddKingshotIngest(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<IngestOptions>()
            .Bind(configuration.GetSection(IngestOptions.SectionName))
            .Validate(o => o.QueueCapacity > 0 && o.MaxMessageBytes > 0, "Ingest:QueueCapacity and Ingest:MaxMessageBytes must be positive.")
            .ValidateOnStart();

        services.AddSingleton<IIngestQueue, ChannelIngestQueue>();

        services.AddSingleton<ISchemaBundleProvider>(sp =>
            new FileSchemaBundleProvider(sp.GetRequiredService<IOptions<IngestOptions>>().Value.SchemaBundlePath));
        services.AddSingleton<SprotoFrameDecoder>();
        services.AddSingleton<FpnnFrameDecoder>();
        services.AddSingleton<IFrameDecoder, FrameDecoder>();

        services.AddHostedService<DecoderWorker>();

        return services;
    }
}
