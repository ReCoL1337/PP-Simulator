namespace Simulator;

public class Orc : Creature
{
    private readonly int baseRage;
    private int rageModifier;

    public int Rage => Validator.Limiter(baseRage + rageModifier, 0, 10);

    public override int Power => Level * 7 + Rage * 3;
    public override string Info => $"{Name} [{Level}][{Rage}]";

    public Orc(string name = "Unknown", int level = 1, int rage = 0)
        : base(name, level)
    {
        baseRage = rage;
        rageModifier = 0;
    }

    public void ModifyRage(int amount)
    {
        rageModifier = Validator.Limiter(rageModifier + amount, -baseRage, 10 - baseRage);
    }

    public override void SayHi() => Console.WriteLine($"Grr... Me {Info}");
}