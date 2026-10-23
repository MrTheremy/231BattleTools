using KingshotTools.Contracts.Frames;
using KingshotTools.Contracts.Protocol;
using KingshotTools.Decoder.Fpnn;
using KingshotTools.Decoder.Sproto;

namespace KingshotTools.Decoder;

/// <summary>Routes a frame to the protocol-specific decoder based on its server port.</summary>
public sealed class FrameDecoder(SprotoFrameDecoder sproto, FpnnFrameDecoder fpnn) : IFrameDecoder
{
    public DecodeResult Decode(CapturedFrame frame)
    {
        ArgumentNullException.ThrowIfNull(frame);

        return ProtocolPorts.Classify(frame.Port) switch
        {
            ProtocolKind.Sproto => sproto.Decode(frame),
            ProtocolKind.Fpnn => fpnn.Decode(frame),
            _ => new DecodeResult(ProtocolKind.Ignored, DecodeStatus.Ignored),
        };
    }
}
