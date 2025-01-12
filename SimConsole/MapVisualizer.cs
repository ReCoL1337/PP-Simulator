using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class MapVisualizer {
    private readonly Map map;
    private readonly Dictionary<Point, List<IMappable>> positions = new();

    public MapVisualizer(Map map) {
        this.map = map;
    }

    public void Clear() {
        positions.Clear();
    }

    public void AddCreature(IMappable creature, Point position) {
        if (!positions.ContainsKey(position))
            positions[position] = new List<IMappable>();
        positions[position].Add(creature);
    }

    private char GetCellContent(Point point) {
        if (!positions.ContainsKey(point))
            return ' ';

        var creatures = positions[point];
        if (creatures.Count > 1)
            return 'X';

        return creatures[0].Symbol;
    }

    public void Draw() {
        Console.WriteLine();
        DrawTopBorder(map.SizeX);

        for (var y = map.SizeY - 1; y >= 0; y--) {
            DrawRow(y, map.SizeX);
            if (y > 0)
                DrawMiddleBorder(map.SizeX);
        }

        DrawBottomBorder(map.SizeX);
        Console.WriteLine();
    }

    private void DrawTopBorder(int size) {
        Console.Write(Box.TopLeft);
        for (var x = 0; x < size; x++) {
            Console.Write(Box.Horizontal);
            if (x < size - 1)
                Console.Write(Box.TopMid);
        }

        Console.WriteLine(Box.TopRight);
    }

    private void DrawBottomBorder(int size) {
        Console.Write(Box.BottomLeft);
        for (var x = 0; x < size; x++) {
            Console.Write(Box.Horizontal);
            if (x < size - 1)
                Console.Write(Box.BottomMid);
        }

        Console.WriteLine(Box.BottomRight);
    }

    private void DrawMiddleBorder(int size) {
        Console.Write(Box.MidLeft);
        for (var x = 0; x < size; x++) {
            Console.Write(Box.Horizontal);
            if (x < size - 1)
                Console.Write(Box.Cross);
        }

        Console.WriteLine(Box.MidRight);
    }

    private void DrawRow(int y, int size) {
        Console.Write(Box.Vertical);
        for (var x = 0; x < size; x++) {
            Console.Write(GetCellContent(new Point(x, y)));
            if (x < size - 1)
                Console.Write(Box.Vertical);
        }

        Console.WriteLine(Box.Vertical);
    }
}