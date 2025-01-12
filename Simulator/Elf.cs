namespace Simulator;

public class Elf : Creature {
    private int agility;
    private int singCount = 0;

    public int Agility {
        get => agility;
        init => agility = Validator.Limiter(value, 0, 10);
    }

    public override int Power => Level * 8 + Agility * 2;

    public override string Info => $"{Name} [{Level}][{Agility}]";

    public Elf(string name = "Unknown", int level = 1, int agility = 0)
        : base(name, level) {
        Agility = agility;
    }

    public Elf() {
    }

    public override void SayHi() {
        Console.WriteLine($"Greetings, I'm {Info}");
    }

    public void Sing() {
        singCount++;
        if (singCount % 3 == 0 && agility < 10)
            agility++;
    }
}