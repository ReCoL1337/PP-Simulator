namespace Simulator.Maps;

public abstract class Map {
    public int SizeX { get; }
    public int SizeY { get; }

    protected Map(int sizeX, int sizeY) {
        if (sizeX < 5) throw new ArgumentOutOfRangeException(nameof(sizeX), "Map width must be at least 5");
        if (sizeY < 5) throw new ArgumentOutOfRangeException(nameof(sizeY), "Map height must be at least 5");
        
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public bool Exist(Point p) {
        return p.X >= 0 && p.X < SizeX && p.Y >= 0 && p.Y < SizeY;
    }

    public abstract Point Next(Point p, Direction d);
    public abstract Point NextDiagonal(Point p, Direction d);
}