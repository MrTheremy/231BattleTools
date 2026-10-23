namespace KingshotTools.Contracts.Frames;

/// <summary>Direction of a frame relative to the game client.</summary>
public enum FrameDirection
{
    /// <summary>Client to game server.</summary>
    Outbound = 0,

    /// <summary>Game server to client.</summary>
    Inbound = 1,
}
