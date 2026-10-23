using KingshotTools.Contracts.Frames;

namespace KingshotTools.Data.Blob;

/// <summary>Archives uploaded raw frame batches so they can be re-decoded later.</summary>
public interface IRawFrameArchive
{
    Task EnsureCreatedAsync(CancellationToken cancellationToken);

    Task ArchiveAsync(FrameBatch batch, CancellationToken cancellationToken);
}
