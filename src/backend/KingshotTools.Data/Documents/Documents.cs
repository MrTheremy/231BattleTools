namespace KingshotTools.Data.Documents;

/// <summary>A decoded game message (container <c>messages</c>).</summary>
public sealed class MessageDocument : DailyGameDocument;

/// <summary>Current state of a map entity: march, rally or map object (container <c>worldState</c>).</summary>
public sealed class WorldStateDocument : GameDocument;

/// <summary>A player profile (container <c>players</c>).</summary>
public sealed class PlayerDocument : GameDocument;

/// <summary>A battle report (container <c>battles</c>).</summary>
public sealed class BattleDocument : GameDocument;

/// <summary>A point-in-time snapshot used for playback (container <c>snapshots</c>).</summary>
public sealed class SnapshotDocument : GameDocument;
