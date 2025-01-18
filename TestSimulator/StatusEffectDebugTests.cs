using Xunit;
using Xunit.Abstractions;
using Simulator;
using Simulator.Maps;
using Simulator.StatusEffects;

namespace TestSimulator;

public class StatusEffectDebugTests
{
    private readonly ITestOutputHelper _output;

    public StatusEffectDebugTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Debug_StatusEffectsCreation()
    {
        // Create creatures
        var orc = new Orc("TestOrc", 1, 5);
        var elf = new Elf("TestElf", 1, 5);

        // Log initial state
        _output.WriteLine($"Initial orc state: {orc}");
        _output.WriteLine($"Initial elf state: {elf}");

        // Add effects
        var rageBoost = new RageBoost(2, 2);
        var agilityBoost = new AgilityBoost(2, 2);

        // Apply effects
        orc.AddStatusEffect(rageBoost);
        elf.AddStatusEffect(agilityBoost);

        // Log after effects
        _output.WriteLine($"\nAfter effects:");
        _output.WriteLine($"Orc state: {orc}");
        _output.WriteLine($"Orc Rage: {orc.Rage}");
        _output.WriteLine($"Elf state: {elf}");
        _output.WriteLine($"Elf effects count: {elf.StatusEffects.Count}");

        // Test cloning
        var simulation = new Simulation(
            new SmallTorusMap(8),
            new List<IMappable> { orc, elf },
            new List<Point> { new(0, 0), new(1, 1) },
            "U");

        var newOrc = simulation.Creatures[0] as Orc;
        _output.WriteLine($"\nCloned orc state: {newOrc}");
        _output.WriteLine($"Cloned orc effects count: {newOrc?.StatusEffects.Count}");
    }
}