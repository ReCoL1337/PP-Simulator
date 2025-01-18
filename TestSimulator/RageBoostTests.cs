using Xunit;
using Xunit.Abstractions;
using Simulator;
using Simulator.Maps;
using Simulator.StatusEffects;

namespace TestSimulator;

public class RageBoostTests
{
    private readonly ITestOutputHelper _output;

    public RageBoostTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void RageBoost_ModifiesOrcStats()
    {
        // Arrange
        var orc = new Orc("TestOrc", 1, 5);
        _output.WriteLine($"Initial orc state: {orc}");

        // Act
        var rageBoost = new RageBoost(2, 2);
        orc.AddStatusEffect(rageBoost);
        
        // Assert & Debug
        _output.WriteLine($"Orc state after boost: {orc}");
        _output.WriteLine($"Current rage value: {orc.Rage}");
        _output.WriteLine($"Status effects count: {orc.StatusEffects.Count}");
        foreach (var effect in orc.StatusEffects)
        {
            _output.WriteLine($"Effect: {effect.GetType().Name}, Duration: {effect.Duration}");
        }
    }
    
    [Fact]
    public void RageBoost_WorksInSimulation()
    {
        // Arrange
        var orc = new Orc("TestOrc", 1, 5);
        var map = new SmallTorusMap(8);
        var creatures = new List<IMappable> { orc };
        var positions = new List<Point> { new(2, 2) };
        
        _output.WriteLine($"Initial orc state: {orc}");
        orc.AddStatusEffect(new RageBoost(2, 2));
        _output.WriteLine($"Orc state after adding effect: {orc}");
        
        // Act
        var simulation = new Simulation(map, creatures, positions, "U");
        _output.WriteLine($"Orc state in simulation creatures: {simulation.Creatures[0]}");
        
        // Create history
        var history = new SimulationHistory(simulation);
        var turn0 = history.GetTurn(0);
        
        // Debug
        _output.WriteLine("\nAvailable keys in turn0 StatusEffects:");
        foreach (var key in turn0.StatusEffects.Keys)
        {
            _output.WriteLine($"Key: '{key}'");
        }
        
        _output.WriteLine("\nCreature states in turn0 Creatures:");
        foreach (var kvp in turn0.Creatures)
        {
            foreach (var creature in kvp.Value)
            {
                _output.WriteLine($"Creature: {creature}");
            }
        }
    }
}