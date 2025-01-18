namespace Simulator.StatusEffects;

public class RageBoost : StatusEffect
{
    private readonly int rageBoost;
    
    public RageBoost(int duration, int rageAmount) : base(duration)
    {
        Name = "Rage Boost";
        Description = $"Increases rage by {rageAmount} for {duration} turns";
        rageBoost = rageAmount;
    }

    public override void Apply(Creature creature)
    {
        if (creature is Orc orc)
        {
            orc.ModifyRage(rageBoost);
        }
    }

    public override void Remove(Creature creature)
    {
        if (creature is Orc orc)
        {
            orc.ModifyRage(-rageBoost);
        }
    }

    public override IStatusEffect Clone()
    {
        return new RageBoost(Duration, rageBoost);
    }
}