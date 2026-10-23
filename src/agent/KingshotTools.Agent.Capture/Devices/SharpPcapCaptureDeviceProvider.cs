namespace KingshotTools.Agent.Capture.Devices;

/// <summary>Enumerates Npcap devices through SharpPcap.</summary>
public sealed class SharpPcapCaptureDeviceProvider : ICaptureDeviceProvider
{
    public IReadOnlyList<ICaptureDevice> GetDevices()
    {
        // TODO: wrap SharpPcap's CaptureDeviceList in ICaptureDevice adapters with a BPF filter for CaptureOptions.Ports.
        return [];
    }
}
