using System.Text.Json;
using KingshotTools.Contracts.Frames;

namespace KingshotTools.Contracts.Tests;

public class FrameBatchSerializationTests
{
    private static readonly JsonSerializerOptions Web = new(JsonSerializerDefaults.Web);

    [Fact]
    public void FrameBatch_round_trips_through_web_json()
    {
        var frame = new CapturedFrame(
            new DateTimeOffset(2026, 10, 23, 12, 0, 0, TimeSpan.Zero),
            FrameDirection.Inbound,
            30101,
            "conn-1",
            [0x00, 0x02, 0xAB, 0xCD]);
        var batch = new FrameBatch("agent-1", "session-1", 42, [frame]);

        var json = JsonSerializer.Serialize(batch, Web);
        var back = JsonSerializer.Deserialize<FrameBatch>(json, Web);

        Assert.NotNull(back);
        Assert.Equal(("agent-1", "session-1", 42L), (back.AgentId, back.SessionId, back.SequenceNumber));
        var only = Assert.Single(back.Frames);
        Assert.Equal(frame.Timestamp, only.Timestamp);
        Assert.Equal(frame.Direction, only.Direction);
        Assert.Equal(frame.Port, only.Port);
        Assert.Equal(frame.ConnectionId, only.ConnectionId);
        Assert.Equal(frame.Payload, only.Payload);
    }
}
