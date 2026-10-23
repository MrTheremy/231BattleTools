using KingshotTools.Contracts.Frames;

namespace KingshotTools.Decoder;

/// <summary>Decodes one captured frame into a JSON message.</summary>
public interface IFrameDecoder
{
    DecodeResult Decode(CapturedFrame frame);
}
