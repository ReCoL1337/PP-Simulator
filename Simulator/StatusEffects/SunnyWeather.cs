namespace Simulator.StatusEffects;

public class SunnyEffect : WeatherEffect
{
    public SunnyEffect(int duration) : base(duration)
    {
        Name = "Sunny Weather";
        Description = $"Sunny weather for {duration} turns. Birds fly faster, elves gain agility.";
    }

    public override Weather WeatherType => Weather.Sunny;

    public override void Apply(Creature target)
    {
        if (target is Elf elf)
        {
            target.AddStatusEffect(new AgilityBoost(Duration, 1));
        }
    }

    public override void Remove(Creature target)
    {
        // Status effects will be removed automatically when they expire
    }

    public override IStatusEffect Clone()
    {
        return new SunnyEffect(Duration);
    }
}