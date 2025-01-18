namespace Simulator.StatusEffects;

public class StormyEffect : WeatherEffect
{
    public StormyEffect(int duration) : base(duration)
    {
        Name = "Stormy Weather";
        Description = $"Stormy weather for {duration} turns. Birds can't fly well, orcs gain rage.";
    }

    public override Weather WeatherType => Weather.Stormy;

    public override void Apply(Creature target)
    {
        if (target is Elf)
        {
            target.AddStatusEffect(new AgilityBoost(Duration, -1));
        }
        else if (target is Orc)
        {
            target.AddStatusEffect(new RageBoost(Duration, 2));
        }
    }

    public override void Remove(Creature target)
    {
        // Status effects will be removed automatically when they expire
    }

    public override IStatusEffect Clone()
    {
        return new StormyEffect(Duration);
    }
}