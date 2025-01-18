namespace Simulator.StatusEffects;

public interface IStatusEffect
{
    string Name { get; }
    string Description { get; }
    int Duration { get; }
    bool IsExpired { get; }
    void Apply(Creature target);
    void Remove(Creature target);
    void Tick();
    IStatusEffect Clone();
}