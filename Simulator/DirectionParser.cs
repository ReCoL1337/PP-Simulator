namespace Simulator;

public static class DirectionParser {
    public static List<Direction> Parse(string input) {
        if (string.IsNullOrEmpty(input))
            return new List<Direction>();

        var directions = new List<Direction>();
        var invalidChars = new List<char>();

        foreach (var c in input.ToUpper()) {
            switch (c) {
                case 'U':
                    directions.Add(Direction.Up);
                    break;
                case 'R':
                    directions.Add(Direction.Right);
                    break;
                case 'D':
                    directions.Add(Direction.Down);
                    break;
                case 'L':
                    directions.Add(Direction.Left);
                    break;
                default:
                    invalidChars.Add(c);
                    break;
            }
        }

        if (invalidChars.Count > 0)
            throw new ArgumentException(
                $"Invalid direction characters: {string.Join(", ", invalidChars)}");

        return directions;
    }
}