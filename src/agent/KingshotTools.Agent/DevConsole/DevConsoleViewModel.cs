using KingshotTools.Agent.DevConsole.DecodePreview;
using KingshotTools.Agent.DevConsole.FrameHex;
using KingshotTools.Agent.DevConsole.Packets;
using KingshotTools.Agent.DevConsole.Recording;
using KingshotTools.Agent.DevConsole.UploadSwitch;
using KingshotTools.Agent.Upload;

namespace KingshotTools.Agent.DevConsole;

/// <summary>Root view model of the dev console window.</summary>
public sealed class DevConsoleViewModel(IDecodePreview decodePreview, UploadOptions uploadOptions)
{
    public PacketListViewModel Packets { get; } = new();

    public FrameHexViewModel FrameHex { get; } = new();

    public DecodePreviewViewModel DecodePreview { get; } = new(decodePreview);

    public RecordingViewModel Recording { get; } = new();

    public UploadTargetViewModel UploadTarget { get; } = new(uploadOptions);
}
