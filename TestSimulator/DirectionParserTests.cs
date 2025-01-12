using Simulator;
using Xunit;

namespace TestSimulator;

public class DirectionParserTests {
    [Fact]
    public void Parse_EmptyString_ReturnsEmptyList() {
        var result = DirectionParser.Parse("");
        Assert.Empty(result);
    }

    [Fact]
    public void Parse_ValidDirections_ReturnsCorrectList() {
        var result = DirectionParser.Parse("UDLR");
        Assert.Equal(4, result.Count);
        Assert.Equal(Direction.Up, result[0]);
        Assert.Equal(Direction.Down, result[1]);
        Assert.Equal(Direction.Left, result[2]);
        Assert.Equal(Direction.Right, result[3]);
    }

    [Fact]
    public void Parse_InvalidCharacters_ThrowsArgumentException() {
        var ex = Assert.Throws<ArgumentException>(() => DirectionParser.Parse("UDX"));
        Assert.Contains("Invalid direction characters: X", ex.Message);
    }

    [Fact]
    public void Parse_MultipleInvalidCharacters_ThrowsArgumentException() {
        var ex = Assert.Throws<ArgumentException>(() => DirectionParser.Parse("UD1X2"));
        Assert.Contains("Invalid direction characters: 1, X, 2", ex.Message);
    }

    [Theory]
    [InlineData("u", Direction.Up)]
    [InlineData("d", Direction.Down)]
    [InlineData("l", Direction.Left)]
    [InlineData("r", Direction.Right)]
    public void Parse_LowercaseCharacters_WorksCorrectly(string input, Direction expected) {
        var result = DirectionParser.Parse(input);
        Assert.Single(result);
        Assert.Equal(expected, result[0]);
    }
}