using Azure.Storage.Blobs;
using KingshotTools.Contracts.Frames;
using KingshotTools.Data.Options;
using Microsoft.Extensions.Options;

namespace KingshotTools.Data.Blob;

public sealed class BlobRawFrameArchive(BlobServiceClient client, IOptions<BlobOptions> options) : IRawFrameArchive
{
    public Task EnsureCreatedAsync(CancellationToken cancellationToken) =>
        client.GetBlobContainerClient(options.Value.RawFramesContainer)
            .CreateIfNotExistsAsync(cancellationToken: cancellationToken);

    public Task ArchiveAsync(FrameBatch batch, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(batch);

        // TODO: write {agentId}/{sessionId}/{sequenceNumber:D10}.bin (compressed) into RawFramesContainer.
        return Task.CompletedTask;
    }
}
