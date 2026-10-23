using CommunityToolkit.Mvvm.ComponentModel;

namespace KingshotTools.Agent.Tray;

/// <summary>State shown in the tray icon tooltip and menu.</summary>
public sealed partial class TrayViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string StatusText { get; set; } = "Kingshot Tools: idle";
}
