using Simulator.Maps;
using Simulator.StatusEffects;

namespace Simulator;

public class SimulationHistory
{
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    
    public SimulationHistory(Simulation simulation)
    {
        // Record initial state
        RecordState(simulation);
        
        // Create a copy of the simulation that will run the moves
        var sim = new Simulation(
            simulation.Map,
            simulation.Creatures,
            simulation.Positions,
            simulation.Moves);

        // Record each turn, including final state
        while (!sim.Finished)
        {
            sim.Turn();
            RecordState(sim);
        }
    }

    private void RecordState(Simulation simulation)
    {
        var creatures = new Dictionary<Point, List<IMappable>>();
        var statusEffects = new Dictionary<string, List<IStatusEffect>>();
        
        for (var i = 0; i < simulation.Creatures.Count; i++)
        {
            var position = simulation.Positions[i];
            var creature = simulation.Creatures[i];
            
            if (!creatures.ContainsKey(position))
                creatures[position] = new List<IMappable>();
            
            creatures[position].Add(creature);

            if (creature is Creature c)
            {
                var effects = c.StatusEffects.ToList();
                if (effects.Any())
                {
                    statusEffects[creature.ToString()] = effects;
                }
            }
        }
        
        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = simulation.CurrentCreature.ToString(),
            Move = simulation.CurrentMoveName,
            Weather = simulation.CurrentWeather,
            Creatures = creatures,
            StatusEffects = statusEffects
        });
    }
    
    public SimulationTurnLog GetTurn(int turnNumber)
    {
        if (turnNumber < 0 || turnNumber >= TurnLogs.Count)
            throw new ArgumentOutOfRangeException(nameof(turnNumber));
        return TurnLogs[turnNumber];
    }
}