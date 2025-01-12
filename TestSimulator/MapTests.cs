using Simulator;
using Xunit;
using Simulator.Maps;

namespace TestSimulator;

public class MapTests {
    private class TestMap : Map {
        public TestMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }
        public override Point Next(Point p, Direction d) => p;
        public override Point NextDiagonal(Point p, Direction d) => p;
    }

    [Theory]
    [InlineData(4)]
    [InlineData(101)]
    public void Constructor_InvalidSize_ThrowsArgumentOutOfRangeException(int size) {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TestMap(size, size));
    }

    [Theory]
    [InlineData(5)]
    [InlineData(20)]
    [InlineData(100)]
    public void Constructor_ValidSize_CreatesMap(int size) {
        var map = new TestMap(size, size);
        Assert.Equal(size, map.SizeX);
        Assert.Equal(size, map.SizeY);
    }

    [Theory]
    [InlineData(0, 0, 10, true)]
    [InlineData(9, 9, 10, true)]
    [InlineData(-1, 0, 10, false)]
    [InlineData(0, -1, 10, false)]
    [InlineData(10, 0, 10, false)]
    [InlineData(0, 10, 10, false)]
    public void Exist_ReturnsCorrectResult(int x, int y, int mapSize, bool expected) {
        var map = new TestMap(mapSize, mapSize);
        var point = new Point(x, y);
        Assert.Equal(expected, map.Exist(point));
    }

    [Theory]
    [InlineData(5, 10)]
    [InlineData(10, 5)]
    [InlineData(100, 50)]
    public void Constructor_DifferentDimensions_CreatesMap(int sizeX, int sizeY) {
        var map = new TestMap(sizeX, sizeY);
        Assert.Equal(sizeX, map.SizeX);
        Assert.Equal(sizeY, map.SizeY);
    }

    [Theory]
    [InlineData(4, 10)]
    [InlineData(10, 4)]
    [InlineData(101, 50)]
    [InlineData(50, 101)]
    public void Constructor_InvalidDimensions_ThrowsArgumentOutOfRangeException(int sizeX, int sizeY) {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TestMap(sizeX, sizeY));
    }
}