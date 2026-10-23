using Microsoft.AspNetCore.SignalR;

namespace KingshotTools.Live.Hubs;

/// <summary>Real-time hub; clients join one group per kingdom they watch.</summary>
public sealed class LiveHub : Hub<ILiveClient>
{
    public const string Route = "/hubs/live";

    // TODO: authorize kingdom membership once auth exists.
    public Task JoinKingdom(int mapKid) => Groups.AddToGroupAsync(Context.ConnectionId, KingdomGroups.For(mapKid));

    public Task LeaveKingdom(int mapKid) => Groups.RemoveFromGroupAsync(Context.ConnectionId, KingdomGroups.For(mapKid));
}
