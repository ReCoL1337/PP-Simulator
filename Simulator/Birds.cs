using Simulator;
using Simulator.Maps;

public class Birds : Animals {
    private readonly bool canFly;
    
    public Birds(string description, uint size, bool canFly = true) {
        Description = description;
        Size = size;
        this.canFly = canFly;
    }
    
    public override string ToString() => $"BIRDS: {Description} <{Size}> ({(canFly ? "fly+" : "fly-")})";
    public override char Symbol => canFly ? 'B' : 'b';

    public override Point GetNextPosition(Point current, Direction direction, Map map) {
        if (canFly) {
            // Double move for flying birds
            var midPoint = map.Next(current, direction);
            return map.Next(midPoint, direction);
        }
        else {
            // Single diagonal move for non-flying birds
            return map.NextDiagonal(current, direction);
        }
    }
}