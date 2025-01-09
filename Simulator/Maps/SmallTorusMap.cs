namespace Simulator.Maps
{
    public class SmallTorusMap : Map
    {
        public int Size { get; }

        public SmallTorusMap(int size)
        {
            if (size < 5 || size > 20)
                throw new ArgumentOutOfRangeException(nameof(size), 
                    "Map size must be between 5 and 20");
            Size = size;
        }

        public override bool Exist(Point p)
        {
            return p.X >= 0 && p.X < Size && p.Y >= 0 && p.Y < Size;
        }

        public override Point Next(Point p, Direction d)
        {
            Point next = p.Next(d);
            return new Point(
                (next.X + Size) % Size,
                (next.Y + Size) % Size
            );
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            Point next = p.NextDiagonal(d);
            return new Point(
                (next.X + Size) % Size,
                (next.Y + Size) % Size
            );
        }
    }
}