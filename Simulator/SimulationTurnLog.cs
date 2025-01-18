using Simulator.Maps;
using Simulator.StatusEffects;

namespace Simulator;

/// <summary>
/// State of map after single simulation turn.
/// </summary>
public class SimulationTurnLog
{
    /// <summary>
    /// Text representation of moving object in this turn.
    /// CurrentMappable.ToString()
    /// </summary>
    public required string Mappable { get; init; }
    
    /// <summary>
    /// Text representation of move in this turn.
    /// CurrentMoveName.ToString();
    /// </summary>
    public required string Move { get; init; }
    
    /// <summary>
    /// Current weather in this turn
    /// </summary>
    public required string Weather { get; init; }
    
    /// <summary>
    /// Dictionary of creatures on the map in this turn.
    /// Multiple creatures can occupy the same position.
    /// </summary>
    public required Dictionary<Point, List<IMappable>> Creatures { get; init; }

    /// <summary>
    /// Dictionary of active status effects for each creature.
    /// </summary>
    public Dictionary<string, List<IStatusEffect>> StatusEffects { get; init; } = new();
}