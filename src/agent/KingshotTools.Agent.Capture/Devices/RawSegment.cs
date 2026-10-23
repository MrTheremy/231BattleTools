using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Capture.Devices;

/// <summary>One TCP segment as seen on the wire, before reassembly.</summary>
public sealed record RawSegment(
    DateTimeOffset Timestamp,
    FrameDirection Direction,
    int ServerPort,
    string ConnectionId,
    uint SequenceNumber,
    ReadOnlyMemory<byte> Payload);
