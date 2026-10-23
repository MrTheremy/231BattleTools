using KingshotTools.Agent.DevConsole.UploadSwitch;
using KingshotTools.Agent.Upload;

namespace KingshotTools.Agent.Tests;

public class UploadTargetViewModelTests
{
    [Fact]
    public void Defaults_to_off() => Assert.Equal(UploadTarget.Off, new UploadTargetViewModel(new UploadOptions()).SelectedTarget);

    [Fact]
    public void Selecting_a_target_updates_upload_options()
    {
        var options = new UploadOptions();
        var viewModel = new UploadTargetViewModel(options) { SelectedTarget = UploadTarget.Local };

        Assert.Equal(UploadTarget.Local, options.Target);
    }
}
