using System.Text.Json;
using KingshotTools.Live.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace KingshotTools.Live.Broadcast;

public sealed class SignalRLiveBroadcaster(IHubContext<LiveHub, ILiveClient> hub) : ILiveBroadcaster
{
    public Task BroadcastWorldStateAsync(int mapKid, IReadOnlyList<JsonElement> documents, CancellationToken cancellationToken) =>
        hub.Clients.Group(KingdomGroups.For(mapKid)).WorldStateChanged(documents);
}
