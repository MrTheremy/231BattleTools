namespace KingshotTools.Agent.Capture.Devices;

/// <summary>Lists the capture devices available on this machine.</summary>
public interface ICaptureDeviceProvider
{
    IReadOnlyList<ICaptureDevice> GetDevices();
}
