using KingshotTools.Agent.Upload.Policy;
using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Tests;

public class PortUploadPolicyTests
{
    private readonly PortUploadPolicy _policy = new();

    [Fact]
    public void Game_data_is_uploaded() => Assert.Equal(UploadDecision.Upload, _policy.Decide(Frame(30101)));

    [Fact]
    public void Chat_bodies_are_never_uploaded() => Assert.Equal(UploadDecision.HeaderOnly, _policy.Decide(Frame(13321)));

    [Theory]
    [InlineData(443)]
    [InlineData(8080)]
    public void Other_ports_are_dropped(int port) => Assert.Equal(UploadDecision.Drop, _policy.Decide(Frame(port)));

    private static CapturedFrame Frame(int port) =>
        new(DateTimeOffset.UnixEpoch, FrameDirection.Outbound, port, "conn", [1, 2, 3]);
}
