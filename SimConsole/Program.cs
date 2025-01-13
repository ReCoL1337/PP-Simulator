using System.Text;
using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class Program {
    private static void DisplayHistoryTurn(SimulationTurnLog turn, MapVisualizer visualizer) {
        // Clear console properly
        Console.Clear();
        
        // Display turn info
        Console.WriteLine($"Turn log: {turn.Mappable} goes {turn.Move}");
        Console.WriteLine();

        // Clear and redraw map
        visualizer.Clear();
        foreach (var (position, symbols) in turn.Symbols) {
            if (symbols.Count == 1) {
                visualizer.AddCreature(new DummyMappable(symbols[0]), position);
            }
            else if (symbols.Count > 1) {
                visualizer.AddCreature(new DummyMappable('X'), position);
            }
        }
        visualizer.Draw();
    }

    public static void Main() {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;

        var map = new SmallTorusMap(8);
        List<IMappable> creatures = [
            new Orc("Gorbag"),
            new Elf("Elandor"),
            new Animals { Description = "Rabbits", Size = 5 },
            new Birds("Eagles", 3, true),
            new Birds("Ostriches", 2, false)
        ];

        List<Point> points = [
            new(1, 1),
            new(2, 2),
            new(3, 3),
            new(4, 4),
            new(5, 5)
        ];

        var moves = "UDLRUDLRUDLRUDLRUDLR"; // 20 moves

        Simulation simulation = new(map, creatures, points, moves);
        SimulationHistory history = new(simulation);
        MapVisualizer visualizer = new(map);

        int[] turnsToShow = [5, 10, 15, 20];
        
        foreach (var turn in turnsToShow) {
            Console.WriteLine($"\nTurn {turn}:");
            DisplayHistoryTurn(history.GetTurn(turn), visualizer);
            
            Console.WriteLine("\nPress any key for next turn...");
            Console.ReadKey(true);
        }

        Console.WriteLine("\nSimulation history replay completed.");
    }
}