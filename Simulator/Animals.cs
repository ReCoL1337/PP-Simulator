namespace Simulator;

public class Animals {
    private string description = "Unknown";
        
    public string Description
    {
        get => description;
        init
        {
            if (value == null) return;
            string processed = value.Trim();
                
            if (processed.Length < 3)
                processed = processed.PadRight(3, '#');
                
            if (processed.Length > 15)
                processed = processed.Substring(0, 15).TrimEnd();
                
            if (processed.Length < 3)
                processed = processed.PadRight(3, '#');

            if (char.IsLower(processed[0]))
                processed = char.ToUpper(processed[0]) + processed.Substring(1);

            description = processed;
        }
    }

    public uint Size { get; set; } = 3;

    public string Info => $"{Description} <{Size}>";
}