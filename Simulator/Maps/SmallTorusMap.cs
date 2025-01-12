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
        Point next;
        switch (d) {
            case Direction.Up:
                next = new Point(p.X + 1, p.Y + 1);
                break;
            case Direction.Right:
                next = new Point(p.X + 1, p.Y - 1);
                break;
            case Direction.Down:
                next = new Point(p.X - 1, p.Y - 1);
                break;
            case Direction.Left:
                next = new Point(p.X - 1, p.Y + 1);
                break;
            default:
                return p;
        }
        return new Point(
            (next.X + SizeX) % SizeX,
            (next.Y + SizeY) % SizeY
        );
    }
}