namespace Simulator;

public class Animals
{
    private string description = "Unknown";
        
    public string Description
    {
        get => description;
        init
        {
            if (value == null) return;
            description = Validator.Shortener(value.Trim(), 3, 15, '#');
            if (char.IsLower(description[0]))
                description = char.ToUpper(description[0]) + description.Substring(1);
        }
    }

    public uint Size { get; set; } = 3;

    public virtual string Info => $"{Description} <{Size}>";

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}