namespace Simulator.Maps;

public abstract class MappableBase {
    public abstract char Symbol { get; }
    public string Name => GetName();
    protected abstract string GetName();
}