using Simulator.Maps;

namespace Simulator;

public class Animals : MappableBase {
    private string description = "Unknown";

    public string Description {
        get => description;
        init {
            if (value == null) return;
            description = Validator.Shortener(value.Trim(), 3, 15, '#');
            if (char.IsLower(description[0]))
                description = char.ToUpper(description[0]) + description.Substring(1);
        }
    }

    public uint Size { get; set; } = 3;

    public virtual string Info => $"{Description} <{Size}>";

    protected override string GetName() => Description;

    public override char Symbol => 'A';

    public override Point GetNextPosition(Point current, Direction direction, Map map) {
        return map.Next(current, direction);
    }

    public override string ToString() {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}