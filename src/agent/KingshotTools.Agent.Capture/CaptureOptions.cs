using KingshotTools.Contracts.Protocol;

namespace KingshotTools.Agent.Capture;

/// <summary>Settings for the capture pipeline.</summary>
public sealed class CaptureOptions
{
    /// <summary>Name of the network device to open; <c>null</c> lets the agent pick one.</summary>
    public string? DeviceName { get; set; }

    /// <summary>Server ports to capture. 443 is deliberately absent.</summary>
    public IList<int> Ports { get; } = [ProtocolPorts.Sproto, ProtocolPorts.Fpnn];
}
