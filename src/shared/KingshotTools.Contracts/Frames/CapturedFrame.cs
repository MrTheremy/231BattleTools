namespace KingshotTools.Contracts.Frames;

/// <summary>
/// One application-level frame cut out of a reassembled TCP stream
/// (for example one length-prefixed sproto message on port 30101).
/// </summary>
/// <param name="Timestamp">Capture time of the last TCP segment that completed the frame.</param>
/// <param name="Direction">Whether the frame went to or came from the game server.</param>
/// <param name="Port">Server-side TCP port; identifies the protocol (see <see cref="Protocol.ProtocolPorts"/>).</param>
/// <param name="ConnectionId">Agent-local id of the TCP connection the frame belongs to.</param>
/// <param name="Payload">Raw frame bytes, including the protocol's own length/header bytes.</param>
public sealed record CapturedFrame(
    DateTimeOffset Timestamp,
    FrameDirection Direction,
    int Port,
    string ConnectionId,
    byte[] Payload);
