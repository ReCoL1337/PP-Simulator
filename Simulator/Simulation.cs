using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    private int turnNumber = 0;
    private readonly List<Direction> moves;
    private readonly WeatherManager weatherManager;

    public Map Map { get; }
    public List<IMappable> Creatures { get; }
    public List<Point> Positions { get; }
    public string Moves { get; }
    public bool Finished { get; private set; } = false;

    public IMappable CurrentCreature => Creatures[turnNumber % Creatures.Count];
    public string CurrentMoveName => !Finished ? moves[turnNumber % moves.Count].ToString().ToLower() : "end";
    public string CurrentWeather => weatherManager.CurrentWeatherInfo;

    public Simulation(Map map, List<IMappable> creatures, List<Point> positions, string moves, int weatherChangeDuration = 5)
    {
        if (creatures.Count == 0)
            throw new ArgumentException("Creatures list cannot be empty");

        if (creatures.Count != positions.Count)
            throw new ArgumentException("Number of creatures must match number of positions");

        Map = map;
        Positions = new List<Point>(positions);
        Moves = moves;
        this.moves = DirectionParser.Parse(moves);
        weatherManager = new WeatherManager(weatherChangeDuration);

        Creatures = new List<IMappable>();
        foreach (var creature in creatures)
        {
            // Create new creature without copying effects
            IMappable newCreature = creature switch
            {
                Birds birds => birds.Clone(),
                Orc orc => new Orc(orc.Name, orc.Level, orc.Rage),
                Elf elf => new Elf(elf.Name, elf.Level, elf.Agility),
                Animals animals => new Animals { Description = animals.Description, Size = animals.Size },
                _ => creature
            };
            Creatures.Add(newCreature);
        }

        if (this.moves.Count == 0)
            Finished = true;

        // Apply effects only after all creatures are created
        for (int i = 0; i < creatures.Count; i++)
        {
            if (creatures[i] is Creature original && Creatures[i] is Creature copy)
            {
                foreach (var effect in original.StatusEffects)
                {
                    copy.AddStatusEffect(effect);
                }
            }
        }

        weatherManager.ApplyWeatherEffects(Creatures);
    }

    public void Turn()
    {
        if (Finished)
            throw new InvalidOperationException("Simulation is finished");

        // Update status effects at start of turn
        foreach (var mappable in Creatures)
        {
            if (mappable is Creature creature)
            {
                var oldEffects = creature.StatusEffects.ToList();
                foreach (var effect in oldEffects)
                {
                    effect.Tick();
                    if (effect.IsExpired)
                    {
                        creature.RemoveStatusEffect(effect);
                    }
                }
            }
        }

        var creatureIndex = turnNumber % Creatures.Count;
        var moveIndex = turnNumber % moves.Count;

        // Check if weather should change
        if (turnNumber > 0 && turnNumber % 5 == 0)
        {
            weatherManager.ChangeWeather();
            weatherManager.ApplyWeatherEffects(Creatures);
        }

        // Get current state
        var currentCreature = Creatures[creatureIndex];
        var position = Positions[creatureIndex];
        var direction = moves[moveIndex];

        // Update position
        Positions[creatureIndex] = currentCreature.GetNextPosition(position, direction, Map);

        // Move to next turn
        turnNumber++;

        // If we've processed all moves for all creatures
        if (turnNumber >= moves.Count * Creatures.Count)
            Finished = true;
    }
    
}