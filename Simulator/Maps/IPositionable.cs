namespace Simulator.Maps;

public interface IPositionable {
    Map? CurrentMap { get; }
    Point? Position { get; }
    void SetPosition(Map map, Point position);
    void Go(Direction direction);
}