namespace Simulator;

public class Creature
{
    private string name = "Unknown";
    private int level = 1;

    public string Name
    {
        get => name;
        init
        {
            if (value == null) return;
            string processed = value.Trim();
                
            if (processed.Length < 3)
                processed = processed.PadRight(3, '#');
                
            if (processed.Length > 25)
                processed = processed.Substring(0, 25).TrimEnd();
                
            if (processed.Length < 3)
                processed = processed.PadRight(3, '#');

            if (char.IsLower(processed[0]))
                processed = char.ToUpper(processed[0]) + processed.Substring(1);

            name = processed;
        }
    }

    public int Level
    {
        get => level;
        init
        {
            level = value switch
            {
                < 1 => 1,
                > 10 => 10,
                _ => value
            };
        }
    }

    public string Info => $"{Name} <{Level}>";

    public Creature(string name = "Unknown", int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature() { }

    public void SayHi()
    {
        Console.WriteLine($"Hi, I'm {Info}");
    }

    public void Upgrade()
    {
        if (level < 10)
            level++;
    }

    public void Go(Direction direction)
    {
        Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}");
    }

    public void Go(Direction[] directions)
    {
        foreach (var direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string path)
    {
        Go(DirectionParser.Parse(path));
    }
}