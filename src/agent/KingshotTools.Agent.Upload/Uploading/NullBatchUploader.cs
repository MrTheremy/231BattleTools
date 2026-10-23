using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Uploading;

/// <summary>Uploader used when the target is <see cref="UploadTarget.Off"/>.</summary>
public sealed class NullBatchUploader : IBatchUploader
{
    public Task UploadAsync(FrameBatch batch, CancellationToken cancellationToken) => Task.CompletedTask;
}
