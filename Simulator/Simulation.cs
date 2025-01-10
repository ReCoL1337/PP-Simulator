using Simulator.Maps;

namespace Simulator;

public class Simulation {
    private int currentCreatureIndex = 0;
        private int currentMoveIndex = 0;
        private readonly Direction[] parsedMoves;

        /// <summary>
        /// Simulation's map.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Creatures moving on the map.
        /// </summary>
        public List<Creature> Creatures { get; }

        /// <summary>
        /// Starting positions of creatures.
        /// </summary>
        public List<Point> Positions { get; }

        /// <summary>
        /// Cyclic list of creatures moves.
        /// </summary>
        public string Moves { get; }

        /// <summary>
        /// Have all moves been done?
        /// </summary>
        public bool Finished { get; private set; } = false;

        /// <summary>
        /// Creature which will be moving current turn.
        /// </summary>
        public Creature CurrentCreature => Creatures[currentCreatureIndex];

        /// <summary>
        /// Lowercase name of direction which will be used in current turn.
        /// </summary>
        public string CurrentMoveName => parsedMoves[currentMoveIndex].ToString().ToLower();

        /// <summary>
        /// Simulation constructor.
        /// </summary>
        public Simulation(Map map, List<Creature> creatures, List<Point> positions, string moves)
        {
            if (creatures.Count == 0)
                throw new ArgumentException("Creatures list cannot be empty");
            
            if (creatures.Count != positions.Count)
                throw new ArgumentException("Number of creatures must match number of positions");

            Map = map;
            Creatures = creatures;
            Positions = positions;
            Moves = moves;
            
            parsedMoves = DirectionParser.Parse(moves);
            if (parsedMoves.Length == 0)
                Finished = true;
        }

        /// <summary>
        /// Makes one move of current creature in current direction.
        /// </summary>
        public void Turn()
        {
            if (Finished)
                throw new InvalidOperationException("Simulation is finished");

            // Update position
            Positions[currentCreatureIndex] = Map.Next(
                Positions[currentCreatureIndex], 
                parsedMoves[currentMoveIndex]
            );

            // Update indices
            currentCreatureIndex = (currentCreatureIndex + 1) % Creatures.Count;
            if (currentCreatureIndex == 0)
            {
                currentMoveIndex = (currentMoveIndex + 1) % parsedMoves.Length;
                if (currentMoveIndex == 0)
                    Finished = true;
            }
        }
}