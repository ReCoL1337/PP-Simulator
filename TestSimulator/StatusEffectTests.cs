using Xunit;
using Simulator;
using Simulator.StatusEffects;
using Xunit.Abstractions;

namespace TestSimulator;

public class StatusEffectTests
{
    private readonly ITestOutputHelper _output;

    public StatusEffectTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void RageBoost_StatsModification_Debug()
    {
        var orc = new Orc("TestOrc", 1, 5);
        _output.WriteLine($"Initial rage: {orc.Rage}");

        var rageBoost = new RageBoost(2, 2);
        orc.AddStatusEffect(rageBoost);
        _output.WriteLine($"After applying boost - Rage: {orc.Rage}");

        orc.UpdateStatusEffects(); // Turn 1
        _output.WriteLine($"After turn 1 - Rage: {orc.Rage}, Effect count: {orc.StatusEffects.Count}");

        orc.UpdateStatusEffects(); // Turn 2
        _output.WriteLine($"After turn 2 - Rage: {orc.Rage}, Effect count: {orc.StatusEffects.Count}");

        orc.UpdateStatusEffects(); // Turn 3
        _output.WriteLine($"After turn 3 - Rage: {orc.Rage}, Effect count: {orc.StatusEffects.Count}");
    }

    [Fact]
    public void AgilityBoost_StatsModification_Debug()
    {
        var elf = new Elf("TestElf", 1, 5);
        _output.WriteLine($"Initial agility: {elf.Agility}");

        var agilityBoost = new AgilityBoost(2, 2);
        elf.AddStatusEffect(agilityBoost);
        _output.WriteLine($"After applying boost - Agility: {elf.Agility}");

        elf.UpdateStatusEffects(); // Turn 1
        _output.WriteLine($"After turn 1 - Agility: {elf.Agility}, Effect count: {elf.StatusEffects.Count}");

        elf.UpdateStatusEffects(); // Turn 2
        _output.WriteLine($"After turn 2 - Agility: {elf.Agility}, Effect count: {elf.StatusEffects.Count}");

        elf.UpdateStatusEffects(); // Turn 3
        _output.WriteLine($"After turn 3 - Agility: {elf.Agility}, Effect count: {elf.StatusEffects.Count}");
    }

    [Fact]
    public void StatusEffect_Remove_ShouldRevertStats()
    {
        // Arrange
        var orc = new Orc("TestOrc", 1, 5);
        var rageBoost = new RageBoost(3, 2);
        var initialRage = orc.Rage;
        
        // Act & Assert
        orc.AddStatusEffect(rageBoost);
        Assert.Equal(initialRage + 2, orc.Rage);
        
        rageBoost.Remove(orc);
        Assert.Equal(initialRage, orc.Rage);
    }

    [Fact]
    public void Creature_RemoveStatusEffect_ShouldCleanupCorrectly()
    {
        // Arrange
        var elf = new Elf("TestElf", 1, 5);
        var agilityBoost = new AgilityBoost(3, 2);
        var initialAgility = elf.Agility;
        
        // Act
        elf.AddStatusEffect(agilityBoost);
        Assert.Single(elf.StatusEffects);
        Assert.Equal(initialAgility + 2, elf.Agility);
        
        elf.RemoveStatusEffect(agilityBoost);
        
        // Assert
        Assert.Empty(elf.StatusEffects);
        Assert.Equal(initialAgility, elf.Agility);
    }

    [Fact]
    public void StatusEffect_Expiration_ShouldRevertStats()
    {
        // Arrange
        var orc = new Orc("TestOrc", 1, 5);
        var initialRage = orc.Rage;
        var rageBoost = new RageBoost(2, 2);
        
        // Act & Debug
        orc.AddStatusEffect(rageBoost);
        _output.WriteLine($"After adding - Rage: {orc.Rage}, Effects: {orc.StatusEffects.Count}");
        
        orc.UpdateStatusEffects(); // First turn
        _output.WriteLine($"Turn 1 - Rage: {orc.Rage}, Effects: {orc.StatusEffects.Count}");
        
        orc.UpdateStatusEffects(); // Second turn (should expire)
        _output.WriteLine($"Turn 2 - Rage: {orc.Rage}, Effects: {orc.StatusEffects.Count}");
        
        // Assert
        Assert.Empty(orc.StatusEffects);
        Assert.Equal(initialRage, orc.Rage);
    }

    [Fact]
    public void StatusEffect_MultipleEffects_ShouldHandleCorrectly()
    {
        // Arrange
        var elf = new Elf("TestElf", 1, 5);
        var initialAgility = elf.Agility;
        
        // Act - Add first boost
        var boost1 = new AgilityBoost(3, 2);
        elf.AddStatusEffect(boost1);
        Assert.Equal(initialAgility + 2, elf.Agility);
        
        // Add second boost (should replace first)
        var boost2 = new AgilityBoost(3, 3);
        elf.AddStatusEffect(boost2);
        Assert.Equal(initialAgility + 3, elf.Agility);
        
        // Verify only one effect active
        Assert.Single(elf.StatusEffects);
    }

    [Fact]
    public void Creature_UpdateStatusEffects_Order()
    {
        var elf = new Elf("TestElf", 1, 5);
        var initialAgility = elf.Agility;
        var boost = new AgilityBoost(2, 2);
        
        elf.AddStatusEffect(boost);
        _output.WriteLine($"Initial state - Agility: {elf.Agility}, Effects: {elf.StatusEffects.Count}, Expired: {boost.IsExpired}");

        for (int i = 1; i <= 3; i++)
        {
            elf.UpdateStatusEffects();
            _output.WriteLine($"After update {i} - Agility: {elf.Agility}, Effects: {elf.StatusEffects.Count}");
            foreach (var effect in elf.StatusEffects)
            {
                _output.WriteLine($"Effect - Duration: {effect.Duration}, IsExpired: {effect.IsExpired}");
            }
        }

        Assert.Empty(elf.StatusEffects);
        Assert.Equal(initialAgility, elf.Agility);
    }
}