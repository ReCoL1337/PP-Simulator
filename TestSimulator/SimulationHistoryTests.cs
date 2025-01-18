using Simulator;
using Simulator.Maps;
using Simulator.StatusEffects;
using Xunit;
using Xunit.Abstractions;

namespace TestSimulator;

public class SimulationHistoryTests
{
    private readonly ITestOutputHelper _output;

    public SimulationHistoryTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void StatusEffect_Duration_Debug()
    {
        var effect = new RageBoost(5, 2);
        _output.WriteLine($"Initial - Duration: {effect.Duration}, IsExpired: {effect.IsExpired}");

        effect.Tick();
        _output.WriteLine($"After first tick - Duration: {effect.Duration}, IsExpired: {effect.IsExpired}");

        effect.Tick();
        _output.WriteLine($"After second tick - Duration: {effect.Duration}, IsExpired: {effect.IsExpired}");
    }

    [Fact]
    public void StatusEffect_Duration_ShouldDecrementCorrectly()
    {
        // Arrange
        var map = new SmallTorusMap(8);
        var orc = new Orc("TestOrc", 1, 5);
        var creatures = new List<IMappable> { orc };
        var positions = new List<Point> { new(2, 2) };
        var moves = "UDUD"; 

        var effect = new RageBoost(2, 2);
        _output.WriteLine($"Initial effect - Duration: {effect.Duration}, IsExpired: {effect.IsExpired}");

        ((Creature)creatures[0]).AddStatusEffect(effect);
        var simulation = new Simulation(map, creatures, positions, moves);
        var history = new SimulationHistory(simulation);

        _output.WriteLine("\nHistory turn logs:");
        for (int i = 0; i < Math.Min(5, history.TurnLogs.Count); i++)
        {
            var turnLog = history.TurnLogs[i];
            var effects = turnLog.StatusEffects
                .SelectMany(x => x.Value)
                .OfType<RageBoost>()
                .ToList();

            _output.WriteLine($"\nTurn {i}:");
            foreach (var e in effects)
            {
                _output.WriteLine($"Effect - Duration: {e.Duration}, IsExpired: {e.IsExpired}");
            }
        }
    }
}