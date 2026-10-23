using System.Windows;
using KingshotTools.Agent.DevConsole;
using KingshotTools.Agent.Startup;
using KingshotTools.Agent.Tray;

namespace KingshotTools.Agent;

public partial class App : Application
{
    private TrayIconHost? _tray;
    private DevConsoleWindow? _devConsole;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var commandLine = AgentCommandLine.Parse(e.Args);
        var composition = new AgentComposition();

        _tray = new TrayIconHost(new TrayViewModel(), commandLine.DevMode ? ShowDevConsole : null, ExitApplication);
        _tray.Show();

        if (commandLine.DevMode)
        {
            _devConsole = new DevConsoleWindow(composition.CreateDevConsoleViewModel());
            ShowDevConsole();
        }

        // TODO: start capture -> reassembly -> spool -> uploader pipeline from the composition root.
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _tray?.Dispose();
        base.OnExit(e);
    }

    private void ShowDevConsole()
    {
        if (_devConsole is null)
        {
            return;
        }

        _devConsole.Show();
        _devConsole.Activate();
    }

    private void ExitApplication() => Shutdown();
}
