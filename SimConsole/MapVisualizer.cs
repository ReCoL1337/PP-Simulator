using Simulator;
using Simulator.Maps;

namespace SimConsole
{
    public class MapVisualizer
    {
        private readonly Map map;
        private readonly Dictionary<Point, List<Creature>> positions = new();

        public MapVisualizer(Map map)
        {
            this.map = map;
        }

        public void Clear()
        {
            positions.Clear();
        }

        public void AddCreature(Creature creature, Point position)
        {
            if (!positions.ContainsKey(position))
                positions[position] = new List<Creature>();
            positions[position].Add(creature);
        }

        private char GetCellContent(Point point)
        {
            if (!positions.ContainsKey(point))
                return ' ';

            var creatures = positions[point];
            if (creatures.Count > 1)
                return 'X';
            
            return creatures[0] switch
            {
                Orc => 'O',
                Elf => 'E',
                _ => '?'
            };
        }

        public void Draw()
        {
            if (map is not SmallSquareMap squareMap)
                throw new ArgumentException("Only SmallSquareMap is supported");

            Console.WriteLine();  // Add extra line before drawing
            DrawTopBorder(squareMap.Size);
            
            for (int y = squareMap.Size - 1; y >= 0; y--)
            {
                DrawRow(y, squareMap.Size);
                if (y > 0)
                    DrawMiddleBorder(squareMap.Size);
            }
            
            DrawBottomBorder(squareMap.Size);
            Console.WriteLine();  // Add extra line after drawing
        }

        private void DrawTopBorder(int size)
        {
            Console.Write(Box.TopLeft);
            for (int x = 0; x < size; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < size - 1)
                    Console.Write(Box.TopMid);
            }
            Console.WriteLine(Box.TopRight);
        }

        private void DrawBottomBorder(int size)
        {
            Console.Write(Box.BottomLeft);
            for (int x = 0; x < size; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < size - 1)
                    Console.Write(Box.BottomMid);
            }
            Console.WriteLine(Box.BottomRight);
        }

        private void DrawMiddleBorder(int size)
        {
            Console.Write(Box.MidLeft);
            for (int x = 0; x < size; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < size - 1)
                    Console.Write(Box.Cross);
            }
            Console.WriteLine(Box.MidRight);
        }

        private void DrawRow(int y, int size)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < size; x++)
            {
                Console.Write(GetCellContent(new Point(x, y)));
                if (x < size - 1)
                    Console.Write(Box.Vertical);
            }
            Console.WriteLine(Box.Vertical);
        }
    }
}