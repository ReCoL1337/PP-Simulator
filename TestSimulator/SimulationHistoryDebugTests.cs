using Simulator;
using Simulator.Maps;
using Xunit;
using Xunit.Abstractions;

namespace TestSimulator;

public class SimulationHistoryDebugTests
{
    private readonly ITestOutputHelper _output;
    
    public SimulationHistoryDebugTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void DebugSimulationHistory()
    {
        // Setup
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
            new(2, 2),
            new(3, 1),
            new(5, 5),
            new(7, 3),
            new(0, 4)
        };

        var moves = "dlrludluddlrulr";
        var simulation = new Simulation(map, creatures, positions, moves);
        
        // Debug original simulation first
        _output.WriteLine("\nDebug Original Simulation:");
        while (!simulation.Finished)
        {
            var currentCreature = simulation.CurrentCreature;
            var currentMove = simulation.CurrentMoveName;
            _output.WriteLine($"Creature: {currentCreature}, Move: {currentMove}");
            simulation.Turn();
        }
        
        // Now debug history
        simulation = new Simulation(map, creatures, positions, moves);
        var history = new SimulationHistory(simulation);
        
        _output.WriteLine("\nDebug History:");
        for (int i = 0; i < history.TurnLogs.Count; i++)
        {
            var turn = history.GetTurn(i);
            _output.WriteLine($"\nTurn {i}:");
            _output.WriteLine($"  Mappable: {turn.Mappable}");
            _output.WriteLine($"  Move: {turn.Move}");
            _output.WriteLine("  Positions:");
            foreach (var pos in turn.Symbols)
            {
                _output.WriteLine($"    {pos.Value} at {pos.Key}");
            }
        }
    }
}