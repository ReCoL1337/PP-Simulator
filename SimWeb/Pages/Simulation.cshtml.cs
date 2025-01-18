using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator;
using Simulator.Maps;
using Simulator.StatusEffects;

namespace SimWeb.Pages;

public class SimulationModel : PageModel
{
    private const string CURRENT_TURN_COOKIE = "CurrentTurn";
    private const int MAX_TURNS = 20;
    private static SimulationHistory? history;
    private static Dictionary<string, Point> originalPositions = new();
    
    private bool AreCreaturesOfSameType(IMappable a, IMappable b)
    {
        // Check if they're the exact same type
        if (a.GetType() != b.GetType()) return false;

        // If they're birds or animals, check their descriptions match
        if (a is Birds birdA && b is Birds birdB)
        {
            return birdA.Name == birdB.Name;
        }
        if (a is Animals animalA && b is Animals animalB)
        {
            return animalA.Description == animalB.Description;
        }

        return true;
    }

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
        foreach (var (pos, creatures) in turnLog.Creatures)
        {
            if (creatures.Any(c => c.ToString() == creatureName))
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
                new(2, 2),    // Orc
                new(3, 1),    // Elf
                new(5, 5),    // Rabbits
                new(7, 3),    // Eagles
                new(0, 4)     // Ostriches
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

    public (string image, string alt, int count) GetCellContent(Point point)
    {
        if (history == null || CurrentTurn >= history.TurnLogs.Count)
            return ("", "", 0);

        var turnLog = history.GetTurn(CurrentTurn);
        if (!turnLog.Creatures.ContainsKey(point))
            return ("", "", 0);

        var creatures = turnLog.Creatures[point];
        if (creatures.Count == 0)
            return ("", "", 0);

        // If there's more than one different type of creature
        if (creatures.Count > 1 && creatures.Select(c => c.GetType()).Distinct().Count() > 1)
        {
            return ("combo-80.png", string.Join(",", creatures.Select(c => c.ToString())), 
                creatures.Sum(x => x is Animals animGroup ? (int)animGroup.Size : 1));
        }

        // Single type of creature - use the first one
        var creature = creatures[0];
        var count = creature is Animals animalSingle ? (int)animalSingle.Size : creatures.Count;

        var imageName = creature switch
        {
            Birds birdType when birdType.ToString().Contains("Eagles") => "eagle-80.png",
            Birds birdType when birdType.ToString().Contains("Ostriches") => "emu-80.png",
            Orc => "orc-80.png",
            Elf => "elf-80.png",
            Animals animType when animType.Description == "Rabbits" => "rabbit-80.png",
            _ => "combo-80.png"
        };

        return (imageName, creature.ToString(), count);
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
    
    public IEnumerable<(string name, string type, string stats, string position, string effects)> GetCreatureStats()
{
    if (history == null || CurrentTurn >= history.TurnLogs.Count)
        return Enumerable.Empty<(string, string, string, string, string)>();

    var turnLog = history.GetTurn(CurrentTurn);
    var stats = new List<(string name, string type, string stats, string position, string effects)>();

    foreach (var (position, creatures) in turnLog.Creatures)
    {
        foreach (var creature in creatures.DistinctBy(c => c.ToString()))
        {
            var count = creatures.Count(c => c.ToString() == creature.ToString());
            
            var creatureType = creature switch
            {
                Birds b when b.ToString().Contains("Eagles") => "Birds",
                Birds b when b.ToString().Contains("Ostriches") => "Birds",
                Animals a when a.Description == "Rabbits" => "Animals",
                _ => creature.GetType().Name
            };

            var statsStr = creature switch
            {
                Orc orc => $"Level: {orc.Level}, Rage: {orc.Rage}, Power: {orc.Power}",
                Elf elf => $"Level: {elf.Level}, Agility: {elf.Agility}, Power: {elf.Power}",
                Birds birds => $"Size: {birds.Size}, Can Fly: {(birds.ToString().Contains("fly+") ? "Yes" : "No")}",
                Animals animals => $"Size: {animals.Size}",
                _ => string.Empty
            };

            var name = count > 1 ? $"{creature.Name} (x{count})" : creature.Name;

            // Collect status effects for this creature
            var creatureKey = creature.ToString();
            var effects = string.Empty;

            // Check if this specific creature instance has status effects
            if (turnLog.StatusEffects.TryGetValue(creatureKey, out var statusEffects) && statusEffects.Any())
            {
                // Get names of status effects
                effects = string.Join(", ", statusEffects.Select(e => e.Name));
            }

            stats.Add((
                name: name,
                type: creatureType,
                stats: statsStr,
                position: position.ToString(),
                effects: effects
            ));
        }
    }

    return stats.OrderBy(s => s.type).ThenBy(s => s.name);
}
}