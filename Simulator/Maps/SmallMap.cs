namespace Simulator.Maps;

public abstract class SmallMap : Map {
    private const int MaxSize = 20;
    private readonly Dictionary<Point, List<IMappable>> creatures = new();

    protected SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY) {
        if (sizeX > MaxSize) throw new ArgumentOutOfRangeException(nameof(sizeX), "Small map width cannot exceed 20");
        if (sizeY > MaxSize) throw new ArgumentOutOfRangeException(nameof(sizeY), "Small map height cannot exceed 20");
    }

    public void Add(IMappable creature, Point position) {
        if (!Exist(position))
            throw new ArgumentException("Position does not exist on map", nameof(position));

        if (!creatures.ContainsKey(position))
            creatures[position] = new List<IMappable>();

        creatures[position].Add(creature);
        ((IPositionable)creature).SetPosition(this, position);
    }

    public void Remove(IMappable creature, Point position) {
        if (creatures.ContainsKey(position))
            creatures[position].Remove(creature);
    }

    public void Move(IMappable creature, Point from, Point to) {
        Remove(creature, from);
        Add(creature, to);
    }

    public IReadOnlyList<IMappable> At(Point position) {
        return creatures.ContainsKey(position)
            ? creatures[position].AsReadOnly()
            : new List<IMappable>().AsReadOnly();
    }

    public IReadOnlyList<IMappable> At(int x, int y) => At(new Point(x, y));
}