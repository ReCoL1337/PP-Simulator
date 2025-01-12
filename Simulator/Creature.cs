using Simulator.Maps;

namespace Simulator;

public abstract class Creature : Mappable {
    private string name = "Unknown";
    private int level = 1;

    protected string Name {
        get => name;
        init {
            if (value == null) return;
            name = Validator.Shortener(value.Trim(), 3, 25, '#');
            if (char.IsLower(name[0]))
                name = char.ToUpper(name[0]) + name.Substring(1);
        }
    }

    public int Level {
        get => level;
        init => level = Validator.Limiter(value, 1, 10);
    }

    public abstract string Info { get; }
    public abstract int Power { get; }

    public Creature(string name = "Unknown", int level = 1) {
        Name = name;
        Level = level;
    }

    protected Creature() {
    }

    public abstract void SayHi();

    public void Upgrade() {
        if (level < 10)
            level++;
    }

    public override string ToString() {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }

    public override char Symbol => GetType().Name[0];

    protected override string GetName() => name;
}