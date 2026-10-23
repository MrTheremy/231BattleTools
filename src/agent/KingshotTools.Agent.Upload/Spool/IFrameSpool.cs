using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Spool;

/// <summary>Durable local queue of frames waiting to be uploaded, so nothing is lost while offline.</summary>
public interface IFrameSpool
{
    ValueTask EnqueueAsync(CapturedFrame frame, CancellationToken cancellationToken);

    /// <summary>Returns the next batch to upload without removing it.</summary>
    ValueTask<FrameBatch?> PeekBatchAsync(int maxFrames, CancellationToken cancellationToken);

    /// <summary>Removes a batch once the backend has acknowledged it.</summary>
    ValueTask AcknowledgeAsync(long sequenceNumber, CancellationToken cancellationToken);
}
