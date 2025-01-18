namespace Simulator.StatusEffects;

public class AgilityBoost : StatusEffect
{
    private readonly int agilityBoost;
    
    public AgilityBoost(int duration, int agilityAmount) : base(duration)
    {
        Name = "Agility Boost";
        Description = $"Increases agility by {agilityAmount} for {duration} turns";
        agilityBoost = agilityAmount;
    }

    public override void Apply(Creature creature)
    {
        if (creature is Elf elf)
        {
            elf.ModifyAgility(agilityBoost);
        }
    }

    public override void Remove(Creature creature)
    {
        if (creature is Elf elf)
        {
            elf.ModifyAgility(-agilityBoost);
        }
    }

    public override IStatusEffect Clone()
    {
        return new AgilityBoost(Duration, agilityBoost);
    }
}