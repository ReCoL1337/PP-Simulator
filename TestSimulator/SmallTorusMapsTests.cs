using Xunit;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallTorusMapTests {
    [Theory]
    [InlineData(0, 0, Direction.Left, 4, 0)] // Wrap around left
    [InlineData(4, 4, Direction.Right, 0, 4)] // Wrap around right
    [InlineData(2, 2, Direction.Up, 2, 3)] // Normal movement
    public void Next_ShouldWrapAround(int x, int y, Direction direction, int expectedX, int expectedY) {
        var map = new SmallTorusMap(5);
        var point = new Point(x, y);
        var result = map.Next(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), result);
    }

    [Theory]
    [InlineData(0, 0, Direction.Left, 4, 1)] // Wrap around left
    [InlineData(4, 4, Direction.Right, 0, 3)] // Wrap around right
    [InlineData(2, 2, Direction.Up, 3, 3)] // Normal diagonal movement
    public void NextDiagonal_ShouldWrapAround(int x, int y, Direction direction, int expectedX, int expectedY) {
        var map = new SmallTorusMap(5);
        var point = new Point(x, y);
        var result = map.NextDiagonal(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), result);
    }
}