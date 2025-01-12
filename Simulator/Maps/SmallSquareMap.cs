namespace Simulator.Maps;

public class SmallSquareMap : SmallMap {
    public SmallSquareMap(int size) : base(size, size) { }

    public override Point Next(Point p, Direction d) {
        var next = p.Next(d);
        return Exist(next) ? next : p;
    }

    public override Point NextDiagonal(Point p, Direction d) {
        var next = p.NextDiagonal(d);
        return Exist(next) ? next : p;
    }
}