using KingshotTools.Contracts.Frames;
using KingshotTools.Contracts.Protocol;
using KingshotTools.Decoder.Fpnn;
using KingshotTools.Decoder.Schema;
using KingshotTools.Decoder.Sproto;

namespace KingshotTools.Decoder.Tests;

public class FrameDecoderTests
{
    private static readonly FrameDecoder Decoder = new(
        new SprotoFrameDecoder(new FileSchemaBundleProvider(bundlePath: null)),
        new FpnnFrameDecoder());

    [Theory]
    [InlineData(30101, ProtocolKind.Sproto)]
    [InlineData(13321, ProtocolKind.Fpnn)]
    [InlineData(443, ProtocolKind.Ignored)]
    public void Routes_frames_by_port(int port, ProtocolKind expected)
    {
        var result = Decoder.Decode(Frame(port));

        Assert.Equal(expected, result.Protocol);
    }

    [Fact]
    public void Ignored_ports_are_not_decoded() =>
        Assert.Equal(DecodeStatus.Ignored, Decoder.Decode(Frame(443)).Status);

    private static CapturedFrame Frame(int port) =>
        new(DateTimeOffset.UnixEpoch, FrameDirection.Inbound, port, "conn", [0x00, 0x00]);
}
