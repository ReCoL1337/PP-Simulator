using Simulator.Maps;

namespace Simulator;

public static class Program {
    [STAThread]
    public static void Main() {
        Lab5a();
        Lab5b();
    }

    private static void Lab5a() {
        Point p = new(10, 25);
        Console.WriteLine(p.Next(Direction.Right)); // (11, 25)
        Console.WriteLine(p.NextDiagonal(Direction.Right)); // (11, 24)

        try {
            Rectangle r1 = new(1, 1, 1, 5); // Should throw exception
            Console.WriteLine(r1);
        }
        catch (ArgumentException e) {
            Console.WriteLine($"Error: {e.Message}");
        }

        Rectangle r2 = new(5, 1, 1, 10); // Should swap coordinates
        Console.WriteLine(r2); // (1, 1):(5, 10)

        Point testPoint1 = new(3, 5);
        Point testPoint2 = new(0, 0);
        Console.WriteLine($"Contains {testPoint1}: {r2.Contains(testPoint1)}");
        Console.WriteLine($"Contains {testPoint2}: {r2.Contains(testPoint2)}");
    }

    private static void Lab5b() {
        try {
            SmallSquareMap map = new(3); // Should throw exception
        }
        catch (ArgumentOutOfRangeException e) {
            Console.WriteLine($"Error: {e.Message}");
        }

        SmallSquareMap validMap = new(10);
        Point p = new(5, 5);

        Console.WriteLine($"Point {p} exists: {validMap.Exist(p)}");
        Console.WriteLine($"Next Up: {validMap.Next(p, Direction.Up)}");
        Console.WriteLine($"Next Diagonal Right: {validMap.NextDiagonal(p, Direction.Right)}");

        // Test boundary cases
        Point edge = new(9, 9);
        Console.WriteLine($"Edge point {edge} next right: {validMap.Next(edge, Direction.Right)}");
        Console.WriteLine($"Edge point {edge} next diagonal right: {validMap.NextDiagonal(edge, Direction.Right)}");
    }
}