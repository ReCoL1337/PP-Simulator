namespace Simulator;

public class Birds : Animals
{
    public bool CanFly { get; init; } = true;

    public override string Info => 
        $"{Description} ({(CanFly ? "fly+" : "fly-")}) <{Size}>";
}