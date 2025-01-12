using Xunit;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests {
    [Theory]
    [InlineData(0, 0, Direction.Left, 0, 0)] // Edge case - stay at boundary
    [InlineData(4, 4, Direction.Right, 4, 4)] // Edge case - stay at boundary
    [InlineData(2, 2, Direction.Up, 2, 3)] // Normal movement
    public void Next_ShouldHandleBoundaries(int x, int y, Direction direction, int expectedX, int expectedY) {
        var map = new SmallSquareMap(5);
        var point = new Point(x, y);
        var result = map.Next(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), result);
    }

    [Theory]
    [InlineData(0, 0, Direction.Left, 0, 0)] // Edge case - stay at boundary
    [InlineData(4, 4, Direction.Right, 4, 4)] // Edge case - stay at boundary
    [InlineData(2, 2, Direction.Up, 3, 3)] // Normal diagonal movement
    public void NextDiagonal_ShouldHandleBoundaries(int x, int y, Direction direction, int expectedX, int expectedY) {
        var map = new SmallSquareMap(5);
        var point = new Point(x, y);
        var result = map.NextDiagonal(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), result);
    }
}