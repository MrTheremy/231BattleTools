using System.Text.Json;

namespace KingshotTools.Live.Hubs;

/// <summary>Methods the server invokes on connected web clients. Keep in sync with src/web/src/live/liveConnection.ts.</summary>
public interface ILiveClient
{
    Task WorldStateChanged(IReadOnlyList<JsonElement> documents);
}
