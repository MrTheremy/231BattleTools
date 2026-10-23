using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Uploading;

/// <summary>Sends frame batches to the backend ingest endpoint.</summary>
public interface IBatchUploader
{
    /// <summary>Uploads a batch and returns once the backend acknowledged it.</summary>
    Task UploadAsync(FrameBatch batch, CancellationToken cancellationToken);
}
