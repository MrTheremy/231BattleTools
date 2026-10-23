namespace KingshotTools.Agent.DevConsole.Packets;

/// <summary>One row of the dev console packet list.</summary>
public sealed record PacketRow(DateTimeOffset Timestamp, string Direction, int Port, string ConnectionId, int Length);
