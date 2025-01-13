using Simulator;
using Simulator.Maps;

public class Animals : Mappable {
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
    public override string ToString() => $"ANIMALS: {Description} <{Size}>";
    protected override string GetName() => Description;
    public override char Symbol => 'A';
}