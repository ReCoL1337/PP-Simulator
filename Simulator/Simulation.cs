using Simulator.Maps;

namespace Simulator;

public class Simulation {
    private int currentCreatureIndex = 0;
    private int currentMoveIndex = 0;
    private readonly List<Direction> parsedMoves;

    public Map Map { get; }
    public List<IMappable> Creatures { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished { get; private set; } = false;

    public IMappable CurrentCreature => Creatures[currentCreatureIndex];
    public string CurrentMoveName => parsedMoves[currentMoveIndex].ToString().ToLower();

    public Simulation(Map map, List<IMappable> creatures, List<Point> positions, string moves) {
        if (creatures.Count == 0)
            throw new ArgumentException("Creatures list cannot be empty");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures must match number of positions");

        Map = map;
        Creatures = creatures;
        Positions = positions;
        Moves = moves;

        parsedMoves = DirectionParser.Parse(moves);
        if (parsedMoves.Count == 0)
            Finished = true;
    }

    public void Turn() {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished");

        Positions[currentCreatureIndex] = Creatures[currentCreatureIndex]
            .GetNextPosition(Positions[currentCreatureIndex],
                parsedMoves[currentMoveIndex],
                Map);

        currentCreatureIndex = (currentCreatureIndex + 1) % Creatures.Count;
        if (currentCreatureIndex == 0) {
            currentMoveIndex = (currentMoveIndex + 1) % parsedMoves.Count;
            if (currentMoveIndex == 0)
                Finished = true;
        }
    }
}