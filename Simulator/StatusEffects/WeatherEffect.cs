namespace Simulator.StatusEffects;

public abstract class WeatherEffect : StatusEffect
{
    protected WeatherEffect(int duration) : base(duration)
    {
    }

    public abstract Weather WeatherType { get; }
}