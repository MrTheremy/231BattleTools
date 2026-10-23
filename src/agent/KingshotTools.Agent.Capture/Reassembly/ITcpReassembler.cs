using KingshotTools.Agent.Capture.Devices;
using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Capture.Reassembly;

/// <summary>Turns ordered/unordered TCP segments into complete, timed protocol frames.</summary>
public interface ITcpReassembler
{
    /// <summary>Feeds one segment and returns any frames it completed.</summary>
    IReadOnlyList<CapturedFrame> Feed(RawSegment segment);

    /// <summary>Drops buffered state for a closed or reset connection.</summary>
    void Reset(string connectionId);
}
