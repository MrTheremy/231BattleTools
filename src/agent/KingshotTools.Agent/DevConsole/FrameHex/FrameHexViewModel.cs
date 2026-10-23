using CommunityToolkit.Mvvm.ComponentModel;

namespace KingshotTools.Agent.DevConsole.FrameHex;

public sealed partial class FrameHexViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string HexText { get; set; } = string.Empty;

    // TODO: format the selected frame as offset | hex | ASCII.
}
