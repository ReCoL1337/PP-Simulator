namespace Simulator;

public class Orc : Creature {
    private int rage;
    private int huntCount = 0;

    public int Rage {
        get => rage;
        init => rage = Validator.Limiter(value, 0, 10);
    }

    public override int Power => Level * 7 + Rage * 3;

    public override string Info => $"{Name} [{Level}][{Rage}]";

    public Orc(string name = "Unknown", int level = 1, int rage = 0)
        : base(name, level) {
        Rage = rage;
    }

    public Orc() {
    }

    public override void SayHi() {
        Console.WriteLine($"Grr... Me {Info}");
    }

    public void Hunt() {
        huntCount++;
        if (huntCount % 2 == 0 && rage < 10)
            rage++;
    }
}