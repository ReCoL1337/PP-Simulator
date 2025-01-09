using Xunit;
using Simulator;
using Simulator.Maps;
namespace TestSimulator;

public class SmallSquareMapTests {
    
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        var map = new SmallSquareMap(10);
        Assert.Equal(10, map.Size);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(21)]
    public void Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(0, 0, 5, true)]
    [InlineData(4, 4, 5, true)]
    [InlineData(-1, 0, 5, false)]
    [InlineData(5, 5, 5, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y, int size, bool expected)
    {
        var map = new SmallSquareMap(size);
        var point = new Point(x, y);
        Assert.Equal(expected, map.Exist(point));
    }

    [Theory]
    [InlineData(0, 0, Direction.Left, 0, 0)]  // Edge case - stay at boundary
    [InlineData(4, 4, Direction.Right, 4, 4)]  // Edge case - stay at boundary
    [InlineData(2, 2, Direction.Up, 2, 3)]    // Normal movement
    public void Next_ShouldHandleBoundaries(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallSquareMap(5);
        var point = new Point(x, y);
        var result = map.Next(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), result);
    }
}