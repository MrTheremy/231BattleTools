using KingshotTools.Contracts.Protocol;

namespace KingshotTools.Contracts.Tests;

public class ProtocolPortsTests
{
    [Theory]
    [InlineData(30101, ProtocolKind.Sproto)]
    [InlineData(13321, ProtocolKind.Fpnn)]
    [InlineData(443, ProtocolKind.Ignored)]
    [InlineData(80, ProtocolKind.Ignored)]
    [InlineData(0, ProtocolKind.Ignored)]
    public void Classify_maps_known_ports(int port, ProtocolKind expected) =>
        Assert.Equal(expected, ProtocolPorts.Classify(port));
}
