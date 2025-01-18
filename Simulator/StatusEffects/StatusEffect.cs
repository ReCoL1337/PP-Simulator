namespace Simulator.StatusEffects;

public abstract class StatusEffect : IStatusEffect
{
    public string Name { get; protected set; } = "Unknown Effect";
    public string Description { get; protected set; } = "";
    protected int duration;
    
    public int Duration 
    { 
        get => duration;
        protected set => duration = value;
    }
    
    public bool IsExpired => Duration <= 0;
    
    protected StatusEffect(int duration)
    {
        Duration = duration;
    }

    public abstract void Apply(Creature target);
    public abstract void Remove(Creature target);
    
    public void Tick()
    {
        Duration--;
    }

    public abstract IStatusEffect Clone();
}