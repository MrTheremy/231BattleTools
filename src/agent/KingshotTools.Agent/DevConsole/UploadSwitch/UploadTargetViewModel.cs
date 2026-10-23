using CommunityToolkit.Mvvm.ComponentModel;
using KingshotTools.Agent.Upload;

namespace KingshotTools.Agent.DevConsole.UploadSwitch;

/// <summary>Switches the upload target between off, local and prod.</summary>
public sealed partial class UploadTargetViewModel : ObservableObject
{
    private readonly UploadOptions _options;

    public UploadTargetViewModel(UploadOptions options)
    {
        _options = options;
        SelectedTarget = options.Target;
    }

    public IReadOnlyList<UploadTarget> Targets { get; } = Enum.GetValues<UploadTarget>();

    [ObservableProperty]
    public partial UploadTarget SelectedTarget { get; set; }

    partial void OnSelectedTargetChanged(UploadTarget value)
    {
        _options.Target = value;
        // TODO: reconnect the uploader to the new target.
    }
}
