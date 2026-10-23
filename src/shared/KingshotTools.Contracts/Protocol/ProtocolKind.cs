namespace KingshotTools.Contracts.Protocol;

/// <summary>Which wire protocol a TCP port carries.</summary>
public enum ProtocolKind
{
    /// <summary>Not a port we care about; never captured or uploaded.</summary>
    Ignored = 0,

    /// <summary>Game data: 2-byte big-endian length followed by a sproto.pack body.</summary>
    Sproto = 1,

    /// <summary>Chat: 12-byte FPNN header followed by MessagePack. Bodies are never uploaded.</summary>
    Fpnn = 2,
}
