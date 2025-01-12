using Xunit;
using Simulator;

namespace TestSimulator;

public class RectangleTests {
    [Fact]
    public void Constructor_ShouldOrderCoordinates() {
        var rect = new Rectangle(5, 5, 0, 0);
        Assert.Equal(0, rect.X1);
        Assert.Equal(0, rect.Y1);
        Assert.Equal(5, rect.X2);
        Assert.Equal(5, rect.Y2);
    }

    [Theory]
    [InlineData(0, 0, 0, 5)]
    [InlineData(0, 0, 5, 0)]
    public void Constructor_ZeroDimension_ShouldThrowArgumentException(int x1, int y1, int x2, int y2) {
        Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
    }

    [Theory]
    [InlineData(0, 0, 2, 2, 1, 1, true)]
    [InlineData(0, 0, 2, 2, 3, 3, false)]
    [InlineData(0, 0, 2, 2, 0, 0, true)]
    [InlineData(0, 0, 2, 2, 2, 2, true)]
    public void Contains_ShouldReturnCorrectResult(
        int rectX1, int rectY1, int rectX2, int rectY2,
        int pointX, int pointY, bool expected) {
        var rect = new Rectangle(rectX1, rectY1, rectX2, rectY2);
        var point = new Point(pointX, pointY);
        Assert.Equal(expected, rect.Contains(point));
    }

    [Fact]
    public void ToString_ShouldFormatCorrectly() {
        var rect = new Rectangle(1, 2, 3, 4);
        Assert.Equal("(1, 2):(3, 4)", rect.ToString());
    }
}