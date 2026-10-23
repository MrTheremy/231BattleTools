using CommunityToolkit.Mvvm.ComponentModel;

namespace KingshotTools.Agent.DevConsole.DecodePreview;

public sealed partial class DecodePreviewViewModel(IDecodePreview preview) : ObservableObject
{
    public bool IsAvailable => preview.IsAvailable;

    [ObservableProperty]
    public partial string PreviewText { get; set; } = string.Empty;

    // TODO: render the selected frame via preview.Render(frame).
}
