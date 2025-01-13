using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    private int turnNumber = 0;
    private readonly List<Direction> moves;

    public Map Map { get; }
    public List<IMappable> Creatures { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished { get; private set; } = false;

    public IMappable CurrentCreature => Creatures[turnNumber % Creatures.Count];
    public string CurrentMoveName => !Finished ? moves[turnNumber % moves.Count].ToString().ToLower() : "end";

    public Simulation(Map map, List<IMappable> creatures, List<Point> positions, string moves)
    {
        if (creatures.Count == 0)
            throw new ArgumentException("Creatures list cannot be empty");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures must match number of positions");

        Map = map;
        Creatures = new List<IMappable>(creatures);
        Positions = new List<Point>(positions);
        Moves = moves;
        this.moves = DirectionParser.Parse(moves);
        
        if (this.moves.Count == 0)
            Finished = true;
    }

    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished");

        var creatureIndex = turnNumber % Creatures.Count;
        var moveIndex = turnNumber % moves.Count;

        // Get current state
        var creature = Creatures[creatureIndex];
        var position = Positions[creatureIndex];
        var direction = moves[moveIndex];

        // Update position
        Positions[creatureIndex] = creature.GetNextPosition(position, direction, Map);

        // Move to next turn
        turnNumber++;

        // If we've processed all moves for all creatures
        if (turnNumber >= moves.Count * Creatures.Count)
            Finished = true;
    }
}