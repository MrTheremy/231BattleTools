using System.Threading.Channels;
using KingshotTools.Contracts.Frames;
using Microsoft.Extensions.Options;

namespace KingshotTools.Ingest.Queue;

/// <summary>In-process bounded queue. TODO: revisit (e.g. Service Bus) if ingest is scaled out.</summary>
public sealed class ChannelIngestQueue(IOptions<IngestOptions> options) : IIngestQueue
{
    private readonly Channel<FrameBatch> _channel = Channel.CreateBounded<FrameBatch>(
        new BoundedChannelOptions(options.Value.QueueCapacity)
        {
            FullMode = BoundedChannelFullMode.Wait,
            SingleReader = true,
        });

    public ValueTask EnqueueAsync(FrameBatch batch, CancellationToken cancellationToken) =>
        _channel.Writer.WriteAsync(batch, cancellationToken);

    public IAsyncEnumerable<FrameBatch> ReadAllAsync(CancellationToken cancellationToken) =>
        _channel.Reader.ReadAllAsync(cancellationToken);
}
