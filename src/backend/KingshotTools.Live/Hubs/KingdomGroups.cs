using System.Globalization;

namespace KingshotTools.Live.Hubs;

/// <summary>SignalR group naming: one group per kingdom.</summary>
public static class KingdomGroups
{
    public static string For(int mapKid) => string.Create(CultureInfo.InvariantCulture, $"kingdom:{mapKid}");
}
