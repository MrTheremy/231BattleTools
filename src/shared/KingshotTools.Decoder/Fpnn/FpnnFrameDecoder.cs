using KingshotTools.Contracts.Frames;
using KingshotTools.Contracts.Protocol;

namespace KingshotTools.Decoder.Fpnn;

/// <summary>Decoder for port 13321: 12-byte FPNN header followed by a MessagePack body (chat).</summary>
/// <remarks>Only used locally in the agent dev console; chat bodies are never uploaded.</remarks>
public sealed class FpnnFrameDecoder : IFrameDecoder
{
    public DecodeResult Decode(CapturedFrame frame)
    {
        ArgumentNullException.ThrowIfNull(frame);

        // TODO: parse the 12-byte FPNN header (magic, version, flags, mtype, ss, psize, seq) and the MessagePack body.
        return DecodeResult.NotImplemented(ProtocolKind.Fpnn);
    }
}
