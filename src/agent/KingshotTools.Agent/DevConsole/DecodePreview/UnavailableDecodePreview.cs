using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.DevConsole.DecodePreview;

/// <summary>Used in Release builds, which do not ship the decoder.</summary>
public sealed class UnavailableDecodePreview : IDecodePreview
{
    public bool IsAvailable => false;

    public string Render(CapturedFrame frame) => "Decoder not included in this build.";
}
