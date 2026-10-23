using KingshotTools.Contracts.Frames;
using KingshotTools.Contracts.Protocol;
using KingshotTools.Decoder.Schema;

namespace KingshotTools.Decoder.Sproto;

/// <summary>Decoder for port 30101: 2-byte big-endian length followed by a sproto.pack body.</summary>
public sealed class SprotoFrameDecoder(ISchemaBundleProvider schemas) : IFrameDecoder
{
    public DecodeResult Decode(CapturedFrame frame)
    {
        ArgumentNullException.ThrowIfNull(frame);
        _ = schemas;

        // TODO: strip the 2-byte BE length, sproto.unpack the body, read the message header,
        // look the message id up in the schema bundle and decode to JSON.
        return DecodeResult.NotImplemented(ProtocolKind.Sproto);
    }
}
