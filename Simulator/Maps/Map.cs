namespace Simulator.Maps;

public abstract class Map {
    public const int MinSize = 5;
    public const int MaxSize = 100;
    
    public int SizeX { get; }
    public int SizeY { get; }

    protected Map(int sizeX, int sizeY) {
        ValidateSize(sizeX, nameof(sizeX));
        ValidateSize(sizeY, nameof(sizeY));
        
        SizeX = sizeX;
        SizeY = sizeY;
    }

    private static void ValidateSize(int size, string paramName) {
        if (size < MinSize)
            throw new ArgumentOutOfRangeException(paramName, 
                $"Map dimension must be at least {MinSize}");
        if (size > MaxSize)
            throw new ArgumentOutOfRangeException(paramName, 
                $"Map dimension cannot exceed {MaxSize}");
    }

    public bool Exist(Point p) {
        return p.X >= 0 && p.X < SizeX && p.Y >= 0 && p.Y < SizeY;
    }

    public abstract Point Next(Point p, Direction d);
    public abstract Point NextDiagonal(Point p, Direction d);
}