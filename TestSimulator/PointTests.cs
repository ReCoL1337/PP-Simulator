using Simulator;
using Xunit;

namespace TestSimulator;

public class PointTests {
    [Theory]
    [InlineData(1, 2, Direction.Up, 1, 3)]
    [InlineData(1, 2, Direction.Right, 2, 2)]
    [InlineData(1, 2, Direction.Down, 1, 1)]
    [InlineData(1, 2, Direction.Left, 0, 2)]
    public void Next_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY) {
        // Arrange
        var point = new Point(x, y);
        var expected = new Point(expectedX, expectedY);

        // Act
        var result = point.Next(direction);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, 2, Direction.Up, 2, 3)]
    [InlineData(1, 2, Direction.Right, 2, 1)]
    [InlineData(1, 2, Direction.Down, 0, 1)]
    [InlineData(1, 2, Direction.Left, 0, 3)]
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y,
        Direction direction, int expectedX, int expectedY) {
        // Arrange
        var point = new Point(x, y);
        var expected = new Point(expectedX, expectedY);

        // Act
        var result = point.NextDiagonal(direction);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectFormat() {
        // Arrange
        var point = new Point(3, 4);

        // Act
        var result = point.ToString();

        // Assert
        Assert.Equal("(3, 4)", result);
    }
}