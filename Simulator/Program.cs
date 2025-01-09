namespace Simulator;
using Simulator.Maps;

class Program
{
    static void Main(string[] args)
    {
        Lab5a();
        Lab5b();
    }

    static void Lab5a()
    {
        Console.WriteLine("Testing Point movements:");
        Point p = new(10, 25);
        Console.WriteLine(p.Next(Direction.Right));          // (11, 25)
        Console.WriteLine(p.NextDiagonal(Direction.Right));  // (11, 24)

        Console.WriteLine("\nTesting Rectangle creation and point containment:");
        try
        {
            Rectangle r1 = new(0, 0, 5, 5);
            Console.WriteLine($"r1: {r1}");
            Console.WriteLine($"Contains (2,3): {r1.Contains(new Point(2, 3))}");
            Console.WriteLine($"Contains (6,6): {r1.Contains(new Point(6, 6))}");

            Rectangle r2 = new(new Point(5, 5), new Point(0, 0));
            Console.WriteLine($"r2: {r2}"); // Should be same as r1

            Rectangle r3 = new(0, 0, 0, 5);
            Console.WriteLine("This shouldn't print - exception should be thrown");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Caught expected exception: {e.Message}");
        }
    }

    static void Lab5b()
    {
        Console.WriteLine("\nTesting SmallSquareMap:");
        try
        {
            var map = new SmallSquareMap(10);
            Point p = new(5, 5);
            
            Console.WriteLine($"Starting at {p}");
            Console.WriteLine($"Map contains {p}: {map.Exist(p)}");
            
            Point outside = new(-1, -1);
            Console.WriteLine($"Map contains {outside}: {map.Exist(outside)}");
            
            Console.WriteLine($"Moving right: {map.Next(p, Direction.Right)}");
            Console.WriteLine($"Moving diagonal right: {map.NextDiagonal(p, Direction.Right)}");
            
            // Test boundary
            Point edge = new(9, 9);
            Console.WriteLine($"\nTesting boundary at {edge}:");
            Console.WriteLine($"Moving right: {map.Next(edge, Direction.Right)}");
            Console.WriteLine($"Moving diagonal right: {map.NextDiagonal(edge, Direction.Right)}");

            // Test invalid size
            var invalidMap = new SmallSquareMap(30);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"\nCaught expected exception: {e.Message}");
        }
    }
}