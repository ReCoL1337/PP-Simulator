using Xunit;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallMapTests {
    private class TestMap : SmallMap {
        public TestMap(int sizeX, int sizeY) : base(sizeX, sizeY) { }

        public override Point Next(Point p, Direction d) => p;
        public override Point NextDiagonal(Point p, Direction d) => p;
    }

    [Theory]
    [InlineData(4, 10)]
    [InlineData(10, 21)]
    [InlineData(21, 10)]
    public void Constructor_InvalidSize_ShouldThrowArgumentOutOfRangeException(int sizeX, int sizeY) {
        Assert.Throws<ArgumentOutOfRangeException>(() => new TestMap(sizeX, sizeY));
    }

    [Theory]
    [InlineData(0, 0, 5, true)]
    [InlineData(4, 4, 5, true)]
    [InlineData(-1, 0, 5, false)]
    [InlineData(5, 5, 5, false)]
    public void Exist_ShouldReturnCorrectValue(int x, int y, int size, bool expected) {
        var map = new TestMap(size, size);
        var point = new Point(x, y);
        Assert.Equal(expected, map.Exist(point));
    }

    [Fact]
    public void Add_ValidPosition_ShouldAddCreature() {
        var map = new TestMap(5, 5);
        var creature = new Animals { Description = "Test" };
        var position = new Point(2, 2);

        map.Add(creature, position);
        var creaturesAtPosition = map.At(position);

        Assert.Single(creaturesAtPosition);
        Assert.Contains(creature, creaturesAtPosition);
    }

    [Fact]
    public void Remove_ExistingCreature_ShouldRemoveCreature() {
        var map = new TestMap(5, 5);
        var creature = new Animals { Description = "Test" };
        var position = new Point(2, 2);

        map.Add(creature, position);
        map.Remove(creature, position);
        var creaturesAtPosition = map.At(position);

        Assert.Empty(creaturesAtPosition);
    }

    [Fact]
    public void Move_ValidPositions_ShouldMoveCreature() {
        var map = new TestMap(5, 5);
        var creature = new Animals { Description = "Test" };
        var from = new Point(2, 2);
        var to = new Point(3, 3);

        map.Add(creature, from);
        map.Move(creature, from, to);

        Assert.Empty(map.At(from));
        Assert.Contains(creature, map.At(to));
    }
}