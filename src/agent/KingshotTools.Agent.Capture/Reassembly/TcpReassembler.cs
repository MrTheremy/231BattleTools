using KingshotTools.Agent.Capture.Devices;
using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Capture.Reassembly;

/// <summary>Placeholder reassembler.</summary>
public sealed class TcpReassembler : ITcpReassembler
{
    public IReadOnlyList<CapturedFrame> Feed(RawSegment segment)
    {
        ArgumentNullException.ThrowIfNull(segment);

        // TODO: per-connection, per-direction sequence tracking; cut frames by protocol
        // (sproto: 2-byte BE length prefix, FPNN: 12-byte header with payload size).
        return [];
    }

    public void Reset(string connectionId)
    {
        // TODO: release buffers for the connection.
    }
}
