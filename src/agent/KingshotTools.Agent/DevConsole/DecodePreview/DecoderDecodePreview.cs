#if DEV_DECODER
using KingshotTools.Contracts.Frames;
using KingshotTools.Decoder;
using KingshotTools.Decoder.Fpnn;
using KingshotTools.Decoder.Schema;
using KingshotTools.Decoder.Sproto;

namespace KingshotTools.Agent.DevConsole.DecodePreview;

/// <summary>Decode preview backed by KingshotTools.Decoder. Compiled only when DEV_DECODER is defined.</summary>
public sealed class DecoderDecodePreview : IDecodePreview
{
    private readonly IFrameDecoder _decoder;

    public DecoderDecodePreview()
    {
        // TODO: let the dev console pick a schema bundle file.
        var schemas = new FileSchemaBundleProvider(bundlePath: null);
        _decoder = new FrameDecoder(new SprotoFrameDecoder(schemas), new FpnnFrameDecoder());
    }

    public bool IsAvailable => true;

    public string Render(CapturedFrame frame)
    {
        var result = _decoder.Decode(frame);
        return result.Body?.ToJsonString() ?? $"{result.Protocol}: {result.Status} {result.Error}";
    }
}
#endif
