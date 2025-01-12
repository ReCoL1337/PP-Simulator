using Xunit;
using Simulator;

namespace TestSimulator;

public class ValidatorTests {
    [Theory]
    [InlineData(5, 0, 10, 5)]
    [InlineData(-1, 0, 10, 0)]
    [InlineData(15, 0, 10, 10)]
    [InlineData(5, 5, 5, 5)]
    public void Limiter_ShouldReturnCorrectValue(int value, int min, int max, int expected) {
        var result = Validator.Limiter(value, min, max);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("test", 3, 5, '#', "test")]
    [InlineData("t", 3, 5, '#', "t##")]
    [InlineData("toolong", 3, 5, '#', "toolo")]
    [InlineData("   test   ", 3, 5, '#', "test")]
    [InlineData("a", 3, 5, '*', "a**")]
    public void Shortener_ShouldReturnCorrectValue(
        string value, int min, int max, char placeholder, string expected) {
        var result = Validator.Shortener(value, min, max, placeholder);
        Assert.Equal(expected, result);
    }
}