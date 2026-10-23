using KingshotTools.Contracts.Frames;
using KingshotTools.Ingest.Queue;
using Microsoft.Extensions.Options;

namespace KingshotTools.Ingest.Tests;

public class ChannelIngestQueueTests
{
    [Fact]
    public async Task Batches_are_read_in_enqueue_order()
    {
        var queue = new ChannelIngestQueue(Options.Create(new IngestOptions { QueueCapacity = 4 }));
        var token = TestContext.Current.CancellationToken;

        await queue.EnqueueAsync(new FrameBatch("a", "s", 1, []), token);
        await queue.EnqueueAsync(new FrameBatch("a", "s", 2, []), token);

        var read = new List<long>();
        await foreach (var batch in queue.ReadAllAsync(token))
        {
            read.Add(batch.SequenceNumber);
            if (read.Count == 2)
            {
                break;
            }
        }

        Assert.Equal([1L, 2L], read);
    }
}
