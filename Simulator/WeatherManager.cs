using Simulator.Maps;
using Simulator.StatusEffects;

namespace Simulator;

public class WeatherManager
{
    private readonly Random random = new();
    private readonly int weatherChangeDuration;
    private WeatherEffect? currentWeather;

    public WeatherManager(int weatherChangeDuration = 5)
    {
        this.weatherChangeDuration = weatherChangeDuration;
        ChangeWeather();
    }

    public void ChangeWeather()
    {
        currentWeather = (Weather)random.Next(2) switch
        {
            Weather.Sunny => new SunnyEffect(weatherChangeDuration),
            Weather.Stormy => new StormyEffect(weatherChangeDuration),
            _ => throw new ArgumentException("Invalid weather type")
        };
    }

    public void ApplyWeatherEffects(IEnumerable<IMappable> creatures)
    {
        if (currentWeather == null) return;

        foreach (var creature in creatures.OfType<Creature>())
        {
            currentWeather.Apply(creature);
        }
    }

    public string CurrentWeatherInfo => currentWeather?.Name ?? "No weather";
}