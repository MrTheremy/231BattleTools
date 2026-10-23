using System.Text.Json;

namespace KingshotTools.Live.Broadcast;

/// <summary>Pushes changed documents to the clients watching a kingdom.</summary>
public interface ILiveBroadcaster
{
    Task BroadcastWorldStateAsync(int mapKid, IReadOnlyList<JsonElement> documents, CancellationToken cancellationToken);
}
