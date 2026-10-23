using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.DevConsole.DecodePreview;

/// <summary>Renders a frame as decoded text for the dev console. Backed by the decoder only in non-Release builds.</summary>
public interface IDecodePreview
{
    bool IsAvailable { get; }

    string Render(CapturedFrame frame);
}
