namespace Simulator.Maps;

/// <summary>
/// Interface for objects that can be placed on a map.
/// </summary>
public interface IMappable {
    string Name { get; }
    char Symbol { get; }
    Point GetNextPosition(Point current, Direction direction, Map map);
}