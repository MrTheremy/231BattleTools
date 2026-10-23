using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Uploading;

/// <summary>Placeholder for the WebSocket/TLS uploader.</summary>
public sealed class WebSocketBatchUploader(Uri endpoint) : IBatchUploader
{
    public Uri Endpoint { get; } = endpoint;

    public Task UploadAsync(FrameBatch batch, CancellationToken cancellationToken)
    {
        // TODO: keep a ClientWebSocket open to Endpoint, authenticate, send the batch, await the ack, reconnect with backoff.
        throw new NotImplementedException("WebSocket upload is not implemented yet.");
    }
}
