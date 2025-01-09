namespace Simulator.Maps;

public class SmallSquareMap : Map
{
    public int Size { get; }

    public SmallSquareMap(int size)
    {
        if (size < 5 || size > 20)
            throw new ArgumentOutOfRangeException(nameof(size), 
                "Map size must be between 5 and 20");
        Size = size;
    }

    public override bool Exist(Point p)
    {
        return new Rectangle(0, 0, Size - 1, Size - 1).Contains(p);
    }

    public override Point Next(Point p, Direction d)
    {
        Point next = p.Next(d);
        return Exist(next) ? next : p;
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point next = p.NextDiagonal(d);
        return Exist(next) ? next : p;
    }
}