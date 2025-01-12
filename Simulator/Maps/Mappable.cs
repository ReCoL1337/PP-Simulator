namespace Simulator.Maps;

public abstract class Mappable : IMappable, IPositionable {
    public abstract char Symbol { get; }
    public string Name => GetName();
    protected abstract string GetName();
    
    public Map? CurrentMap { get; private set; }
    public Point? Position { get; private set; }

    public void SetPosition(Map map, Point position) {
        CurrentMap = map;
        Position = position;
    }

    public void Go(Direction direction) {
        if (CurrentMap == null || Position == null) return;
        
        var oldPosition = Position.Value;
        var newPosition = GetNextPosition(oldPosition, direction, CurrentMap);
        
        if (CurrentMap is SmallMap smallMap) {
            smallMap.Move(this, oldPosition, newPosition);
        }
        
        Position = newPosition;
    }

    public virtual Point GetNextPosition(Point current, Direction direction, Map map) {
        return map.Next(current, direction);
    }
}