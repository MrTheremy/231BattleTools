using KingshotTools.Data.Blob;
using KingshotTools.Data.Cosmos;
using KingshotTools.Decoder;
using KingshotTools.Ingest.Queue;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KingshotTools.Ingest.Worker;

/// <summary>Background worker that drains the ingest queue.</summary>
public sealed class DecoderWorker(
    IIngestQueue queue,
    IFrameDecoder decoder,
    IRawFrameArchive archive,
    IGameDocumentStore store,
    IOptions<IngestOptions> options,
    ILogger<DecoderWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _ = decoder;
        _ = store;

        await foreach (var batch in queue.ReadAllAsync(stoppingToken))
        {
            logger.LogDebug(
                "Dequeued batch {SequenceNumber} from agent {AgentId} session {SessionId} with {FrameCount} frames",
                batch.SequenceNumber,
                batch.AgentId,
                batch.SessionId,
                batch.Frames.Count);

            if (options.Value.ArchiveRawFrames)
            {
                await archive.ArchiveAsync(batch, stoppingToken);
            }

            // TODO: decode each frame, map messages to documents, upsert via IGameDocumentStore,
            // and record unknown message ids for the Protocol Lab.
        }
    }
}
