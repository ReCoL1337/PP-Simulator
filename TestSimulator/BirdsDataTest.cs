using Simulator;
using Simulator.Maps;
using Xunit;
using Xunit.Abstractions;

namespace TestSimulator;

public class BirdsDataTest
{
    private readonly ITestOutputHelper _output;

    public BirdsDataTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void InspectBirdsInSimulation()
    {
        // Arrange
        var map = new SmallTorusMap(8);
        var birds = new Birds("Eagles", 15, true);
        var creatures = new List<IMappable> { birds };
        var positions = new List<Point> { new(7, 3) };
        
        // Act
        var simulation = new Simulation(map, creatures, positions, "U");
        var history = new SimulationHistory(simulation);
        var turn0 = history.GetTurn(0);
        
        // Debug output
        foreach (var (pos, creatureList) in turn0.Creatures)
        {
            _output.WriteLine($"\nPosition {pos}:");
            _output.WriteLine($"Number of creatures at position: {creatureList.Count}");
            foreach (var c in creatureList)
            {
                _output.WriteLine($"Creature: {c}");
                _output.WriteLine($"Type: {c.GetType().Name}");
                _output.WriteLine($"Name: {c.Name}");
                if (c is Birds b)
                {
                    _output.WriteLine($"Bird Size: {b.Size}");
                }
            }
        }
    }

    [Fact]
    public void TestBirdCreation()
    {
        // Create birds
        var eagles = new Birds("Eagles", 3, true);
        
        // Debug output
        _output.WriteLine($"Single eagle toString: {eagles}");
        
        // Create a list with multiple eagles
        var pos = new Point(7, 3);
        var creatures = new Dictionary<Point, List<IMappable>>();
        creatures[pos] = new List<IMappable>();
        
        // Add multiple eagles
        for (int i = 0; i < eagles.Size; i++)
        {
            creatures[pos].Add(eagles);
        }
        
        _output.WriteLine($"\nNumber of creatures at position: {creatures[pos].Count}");
        foreach (var c in creatures[pos])
        {
            _output.WriteLine($"Creature in list: {c}");
        }
    }
}