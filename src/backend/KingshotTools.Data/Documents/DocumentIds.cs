using System.Globalization;

namespace KingshotTools.Data.Documents;

/// <summary>Builds Cosmos document ids. Pure functions; ids are stable across decoder versions.</summary>
public static class DocumentIds
{
    public static string March(long ownerId, long marchId) => Format($"march:{ownerId}:{marchId}");

    public static string Rally(long ownerId, long rallyId) => Format($"rally:{ownerId}:{rallyId}");

    public static string MapObject(int x, int y) => Format($"obj:{x}:{y}");

    public static string Player(long uid) => Format($"player:{uid}");

    public static string Alliance(long allianceId) => Format($"alliance:{allianceId}");

    private static string Format(FormattableString value) => value.ToString(CultureInfo.InvariantCulture);
}
