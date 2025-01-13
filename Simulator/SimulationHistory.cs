using Simulator.Maps;

namespace Simulator;

public class SimulationHistory
{
    public List<SimulationTurnLog> TurnLogs { get; } = [];
    
    public SimulationHistory(Simulation simulation)
    {
        var sim = new Simulation(
            simulation.Map,
            simulation.Creatures,
            simulation.Positions,
            simulation.Moves);
            
        // Record initial state before any moves
        RecordState(sim);
        
        // Record each turn
        while (!sim.Finished)
        {
            sim.Turn();
            if (!sim.Finished)
            {
                RecordState(sim);
            }
        }
    }
    
    private void RecordState(Simulation simulation)
    {
        var symbols = new Dictionary<Point, List<char>>();
        
        // Group creatures by position
        for (var i = 0; i < simulation.Creatures.Count; i++)
        {
            var position = simulation.Positions[i];
            var symbol = simulation.Creatures[i].Symbol;
            
            if (!symbols.ContainsKey(position))
                symbols[position] = new List<char>();
                
            symbols[position].Add(symbol);
        }
        
        TurnLogs.Add(new SimulationTurnLog
        {
            Mappable = simulation.CurrentCreature.ToString(),
            Move = simulation.CurrentMoveName,
            Symbols = symbols
        });
    }
    
    public SimulationTurnLog GetTurn(int turnNumber)
    {
        if (turnNumber < 0 || turnNumber >= TurnLogs.Count)
            throw new ArgumentOutOfRangeException(nameof(turnNumber));
        return TurnLogs[turnNumber];
    }
}