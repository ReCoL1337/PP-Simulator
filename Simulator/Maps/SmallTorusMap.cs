namespace Simulator.Maps;

public class SmallTorusMap : SmallMap {
    public SmallTorusMap(int size) : base(size, size) { }

    public override Point Next(Point p, Direction d) {
        var next = p.Next(d);
        return new Point(
            (next.X + SizeX) % SizeX,
            (next.Y + SizeY) % SizeY
        );
    }

    public override Point NextDiagonal(Point p, Direction d) {
        var next = p.NextDiagonal(d);
        return new Point(
            (next.X + SizeX) % SizeX,
            (next.Y + SizeY) % SizeY
        );
    }
}