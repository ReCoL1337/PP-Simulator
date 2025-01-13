using Simulator;
using Simulator.Maps;

namespace SimConsole;

public class DummyMappable : IMappable 
{
    private readonly char _symbol;
    
    public DummyMappable(char symbol) 
    {
        _symbol = symbol;
    }
    
    public string Name => _symbol.ToString();
    public char Symbol => _symbol;
    public Point GetNextPosition(Point current, Direction direction, Map map) => current;
}