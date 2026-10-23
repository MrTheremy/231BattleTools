using System.Globalization;
using KingshotTools.Data.Documents;

namespace KingshotTools.Data.Tests;

public class DocumentIdsTests
{
    [Fact]
    public void March_id() => Assert.Equal("march:123:456", DocumentIds.March(123, 456));

    [Fact]
    public void Rally_id() => Assert.Equal("rally:123:789", DocumentIds.Rally(123, 789));

    [Theory]
    [InlineData(0, 0, "obj:0:0")]
    [InlineData(512, 1023, "obj:512:1023")]
    public void Map_object_id(int x, int y, string expected) => Assert.Equal(expected, DocumentIds.MapObject(x, y));

    [Fact]
    public void Player_id() => Assert.Equal("player:9007199254740993", DocumentIds.Player(9007199254740993));

    [Fact]
    public void Alliance_id() => Assert.Equal("alliance:77", DocumentIds.Alliance(77));

    [Fact]
    public void Ids_do_not_depend_on_current_culture()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            // A culture whose negative sign and digit grouping differ from invariant.
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            Assert.Equal("march:-1:1000000", DocumentIds.March(-1, 1_000_000));
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }
}
