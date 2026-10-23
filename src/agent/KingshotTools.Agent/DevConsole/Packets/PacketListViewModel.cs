using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace KingshotTools.Agent.DevConsole.Packets;

public sealed partial class PacketListViewModel : ObservableObject
{
    public ObservableCollection<PacketRow> Packets { get; } = [];

    [ObservableProperty]
    public partial PacketRow? SelectedPacket { get; set; }

    // TODO: subscribe to reassembled frames and append rows (bounded, newest last).
}
