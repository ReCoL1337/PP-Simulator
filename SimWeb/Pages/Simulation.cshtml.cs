using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimConsole;
using Simulator;
using Simulator.Maps;

namespace SimWeb.Pages;

public class SimulationModel : PageModel
{
    private const string CURRENT_TURN_COOKIE = "CurrentTurn";
    private const int MAX_TURNS = 20;
    private static SimulationHistory? history;
    private static Dictionary<string, Point> originalPositions = new();
    
    public int CurrentTurn
    {
        get
        {
            if (int.TryParse(Request.Cookies[CURRENT_TURN_COOKIE], out int turn))
                return Math.Min(turn, MAX_TURNS);
            return 0;
        }
    }
    
    public string Status
    {
        get
        {
            if (CurrentTurn == 0)
                return "Pozycje startowe";
            
            var turnLog = history?.GetTurn(CurrentTurn - 1);
            if (turnLog == null) return string.Empty;

            var creatureName = turnLog.Mappable;
            var position = FindCreaturePosition(turnLog.Mappable, CurrentTurn - 1);
            
            return $"{creatureName} {position} → {turnLog.Move}";
        }
    }
    
    private Point? FindCreaturePosition(string creatureName, int turnNumber)
    {
        if (history == null) return null;
        
        var turnLog = history.GetTurn(turnNumber);
        foreach (var (pos, symbols) in turnLog.Symbols)
        {
            // Check the position that has the first character of the creature name
            char creatureSymbol = creatureName[0];
            if (symbols.Contains(creatureSymbol))
            {
                return pos;
            }
        }
        return null;
    }
    
    public Map Map { get; private set; }

    public SimulationModel()
    {
        Map = new SmallTorusMap(8);
        if (history == null)
        {
            var creatures = new List<IMappable>
            {
                new Orc("Gorbag", 1, 1),
                new Elf("Elandor", 1, 1),
                new Animals { Description = "Rabbits", Size = 10 },
                new Birds("Eagles", 15, true),
                new Birds("Ostriches", 8, false)
            };

            var positions = new List<Point>
            {
                new(2, 2),
                new(3, 1),
                new(5, 5),
                new(7, 3),
                new(0, 4)
            };

            // Store original positions
            for (int i = 0; i < creatures.Count; i++)
            {
                originalPositions[creatures[i].ToString()] = positions[i];
            }

            var simulation = new Simulation(Map, creatures, positions, "dlrludluddlrulr");
            history = new SimulationHistory(simulation);
        }
    }

    public (string image, string alt) GetCellContent(Point point)
    {
        if (history == null || CurrentTurn >= history.TurnLogs.Count)
            return ("", "");

        var turnLog = history.GetTurn(CurrentTurn);
        if (!turnLog.Symbols.ContainsKey(point))
            return ("", "");

        var symbols = turnLog.Symbols[point];

        if (symbols.Count == 0)
            return ("", "");

        if (symbols.Count > 1)
            return ("combo-80.png", string.Join(",", symbols));

        var symbol = symbols[0];
        var imageName = symbol switch
        {
            'O' => "ork-80.png",
            'E' => "elf-80.png",
            'A' => "rabbit-80.png",
            'B' => "eagle-80.png",
            'b' => "emu-80.png",
            _ => $"{symbol.ToString().ToLower()}-80.png"
        };

        return (imageName, symbol.ToString());
    }

    public IActionResult OnPostPrevious()
    {
        int newTurn = CurrentTurn;
        if (newTurn > 0) 
            newTurn--;
            
        Response.Cookies.Append(CURRENT_TURN_COOKIE, newTurn.ToString(), new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(7)
        });
        
        return RedirectToPage();
    }

    public IActionResult OnPostNext()
    {
        int newTurn = CurrentTurn;
        if (history != null && newTurn < Math.Min(history.TurnLogs.Count - 1, MAX_TURNS)) 
            newTurn++;
            
        Response.Cookies.Append(CURRENT_TURN_COOKIE, newTurn.ToString(), new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(7)
        });
        
        return RedirectToPage();
    }
}