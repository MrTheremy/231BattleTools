using System.ComponentModel;
using System.Windows;

namespace KingshotTools.Agent.DevConsole;

public partial class DevConsoleWindow : Window
{
    public DevConsoleWindow(DevConsoleViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        // Closing the console only hides it; the agent keeps running in the tray.
        e.Cancel = true;
        Hide();
        base.OnClosing(e);
    }
}
