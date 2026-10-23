using KingshotTools.Contracts.Frames;

namespace KingshotTools.Ingest.Queue;

/// <summary>Hands uploaded batches from the ingest endpoint to the decoder worker.</summary>
public interface IIngestQueue
{
    ValueTask EnqueueAsync(FrameBatch batch, CancellationToken cancellationToken);

    IAsyncEnumerable<FrameBatch> ReadAllAsync(CancellationToken cancellationToken);
}
