using KingshotTools.Live.Hubs;

namespace KingshotTools.Live.Tests;

public class KingdomGroupsTests
{
    [Fact]
    public void One_group_per_kingdom() => Assert.Equal("kingdom:231", KingdomGroups.For(231));
}
