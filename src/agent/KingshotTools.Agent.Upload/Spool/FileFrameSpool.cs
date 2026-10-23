using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Spool;

/// <summary>Placeholder for a file-backed spool under <see cref="UploadOptions.SpoolDirectory"/>.</summary>
public sealed class FileFrameSpool(UploadOptions options) : IFrameSpool
{
    public ValueTask EnqueueAsync(CapturedFrame frame, CancellationToken cancellationToken)
    {
        _ = options;
        // TODO: append to a segment file; rotate and cap total size.
        return ValueTask.CompletedTask;
    }

    public ValueTask<FrameBatch?> PeekBatchAsync(int maxFrames, CancellationToken cancellationToken)
    {
        // TODO: read the oldest unacknowledged frames and assign the next sequence number.
        return ValueTask.FromResult<FrameBatch?>(null);
    }

    public ValueTask AcknowledgeAsync(long sequenceNumber, CancellationToken cancellationToken)
    {
        // TODO: delete or compact acknowledged segments.
        return ValueTask.CompletedTask;
    }
}
