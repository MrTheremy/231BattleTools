using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace KingshotTools.Agent.DevConsole.Recording;

/// <summary>Record to / load from local .pcapng files. Files never leave the PC (docs/adr/0004).</summary>
public sealed partial class RecordingViewModel : ObservableObject
{
    [ObservableProperty]
    public partial bool IsRecording { get; set; }

    [RelayCommand]
    private void ToggleRecording()
    {
        // TODO: start/stop IPcapngRecorder into samples/ (git-ignored).
    }

    [RelayCommand]
    private void LoadCapture()
    {
        // TODO: pick a .pcapng and replay it through reassembly into the packet list.
    }
}
