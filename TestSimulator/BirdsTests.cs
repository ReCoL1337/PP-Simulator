using Simulator;
using Simulator.Maps;
using Xunit;

namespace TestSimulator;

public class BirdsTests {
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Constructor_SetsPropertiesCorrectly(bool canFly) {
        var birds = new Birds("Test", 5, canFly);
        Assert.Equal("Test", birds.Description);
        Assert.Equal(5u, birds.Size);
        Assert.Contains(canFly ? "fly+" : "fly-", birds.Info);
        Assert.Equal(canFly ? 'B' : 'b', birds.Symbol);
    }

    [Fact]
    public void GetNextPosition_Flying_MovesTwoSteps() {
        var birds = new Birds("Eagles", 3, true);
        var map = new SmallSquareMap(10);
        var start = new Point(5, 5);
        var result = birds.GetNextPosition(start, Direction.Up, map);
        Assert.Equal(new Point(5, 7), result);
    }

    [Fact]
    public void GetNextPosition_NonFlying_MovesDiagonally() {
        var birds = new Birds("Ostrich", 3, false);
        var map = new SmallSquareMap(10);
        var start = new Point(5, 5);
        var result = birds.GetNextPosition(start, Direction.Up, map);
        Assert.Equal(new Point(6, 6), result);
    }

    [Fact]
    public void Info_ContainsBaseInfo() {
        var birds = new Birds("Eagles", 3, true);
        Assert.Contains("Eagles <3>", birds.Info);
    }

    [Theory]
    [InlineData("Eagles", 3, true, "Eagles <3> (fly+)")]
    [InlineData("Ostrich", 5, false, "Ostrich <5> (fly-)")]
    public void Info_FormatsCorrectly(string description, uint size, bool canFly, string expected) {
        var birds = new Birds(description, size, canFly);
        Assert.Equal(expected, birds.Info);
    }

    [Fact]
    public void GetNextPosition_Flying_AtMapBoundary() {
        var birds = new Birds("Eagles", 3, true);
        var map = new SmallSquareMap(10);
        var start = new Point(9, 9);
        var result = birds.GetNextPosition(start, Direction.Up, map);
        Assert.Equal(new Point(9, 9), result); // Should stay at boundary
    }

    [Fact]
    public void GetNextPosition_NonFlying_AtMapBoundary() {
        var birds = new Birds("Ostrich", 3, false);
        var map = new SmallSquareMap(10);
        var start = new Point(9, 9);
        var result = birds.GetNextPosition(start, Direction.Up, map);
        Assert.Equal(new Point(9, 9), result); // Should stay at boundary
    }
}