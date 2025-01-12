using Simulator.Maps;

namespace Simulator;

public class Birds : Animals {
    public bool CanFly { get; init; } = true;

    public override string Info => $"{Description} ({(CanFly ? "fly+" : "fly-")}) <{Size}>";

    public override char Symbol => CanFly ? 'B' : 'b';

    public override Point GetNextPosition(Point current, Direction direction, Map map) {
        if (CanFly) {
            // Flying birds move two steps in the given direction
            var intermediate = map.Next(current, direction);
            return map.Next(intermediate, direction);
        }
        else {
            // Flightless birds move diagonally
            return map.NextDiagonal(current, direction);
        }
    }
}