using Simulator;
using Simulator.Maps;
using Xunit;

namespace TestSimulator;

public class SimulationHistoryTests
{
    [Theory]
    [InlineData(0, "ORC: Gorbag [1][1]", "down")]
    [InlineData(1, "ELF: Elandor [1][1]", "left")]
    [InlineData(2, "ANIMALS: Rabbits <10>", "right")]
    [InlineData(3, "BIRDS: Eagles <15> (fly+)", "left")]
    [InlineData(4, "BIRDS: Ostriches <8> (fly-)", "up")]
    public void FirstFiveTurns_ShouldFollowExpectedPattern(int turn, string expectedMappable, string expectedMove)
    {
        // Arrange
        var map = new SmallTorusMap(8);
        var creatures = new List<IMappable>
        {
            new Orc("Gorbag", 1, 1),
            new Elf("Elandor", 1, 1),
            new Animals { Description = "Rabbits", Size = 10 },
            new Birds("Eagles", 15, true),
            new Birds("Ostriches", 8, false)
        };

        var positions = new List<Point>
        {
            new(2, 2),   // Orc
            new(3, 1),   // Elf
            new(5, 5),   // Rabbits
            new(7, 3),   // Eagles
            new(0, 4)    // Ostriches
        };

        string moves = "dlrlu"; // Each creature gets a different move
        var simulation = new Simulation(map, creatures, positions, moves);
        var history = new SimulationHistory(simulation);

        // Act
        var turnLog = history.GetTurn(turn);

        // Assert
        Assert.Equal(expectedMappable, turnLog.Mappable);
        Assert.Equal(expectedMove, turnLog.Move);
    }
}