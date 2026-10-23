using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KingshotTools.Agent.Tray;

/// <summary>Owns the notification-area icon and its context menu.</summary>
public sealed class TrayIconHost : IDisposable
{
    private readonly NotifyIcon _icon;
    private readonly TrayViewModel _viewModel;

    public TrayIconHost(TrayViewModel viewModel, Action? openDevConsole, Action exit)
    {
        _viewModel = viewModel;

        var menu = new ContextMenuStrip();
        if (openDevConsole is not null)
        {
            menu.Items.Add("Open dev console", null, (_, _) => openDevConsole());
            menu.Items.Add(new ToolStripSeparator());
        }

        menu.Items.Add("Exit", null, (_, _) => exit());

        // TODO: ship a real .ico resource.
        _icon = new NotifyIcon
        {
            Icon = SystemIcons.Application,
            Text = viewModel.StatusText,
            ContextMenuStrip = menu,
        };

        if (openDevConsole is not null)
        {
            _icon.DoubleClick += (_, _) => openDevConsole();
        }

        _viewModel.PropertyChanged += OnViewModelChanged;
    }

    public void Show() => _icon.Visible = true;

    public void Dispose()
    {
        _viewModel.PropertyChanged -= OnViewModelChanged;
        _icon.Visible = false;
        _icon.ContextMenuStrip?.Dispose();
        _icon.Dispose();
    }

    private void OnViewModelChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TrayViewModel.StatusText))
        {
            // NotifyIcon.Text is limited to 127 characters.
            var text = _viewModel.StatusText;
            _icon.Text = text.Length > 127 ? text[..127] : text;
        }
    }
}
