namespace KingshotTools.Contracts.Protocol;

/// <summary>Known Kingshot server ports and the protocol each one carries.</summary>
public static class ProtocolPorts
{
    /// <summary>Game data (sproto).</summary>
    public const int Sproto = 30101;

    /// <summary>Chat (FPNN + MessagePack).</summary>
    public const int Fpnn = 13321;

    /// <summary>HTTPS; explicitly ignored.</summary>
    public const int Https = 443;

    /// <summary>Maps a server-side port to the protocol it carries.</summary>
    public static ProtocolKind Classify(int port) => port switch
    {
        Sproto => ProtocolKind.Sproto,
        Fpnn => ProtocolKind.Fpnn,
        _ => ProtocolKind.Ignored,
    };
}
