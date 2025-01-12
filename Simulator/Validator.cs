namespace Simulator;

public static class Validator {
    public static int Limiter(int value, int min, int max) {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    public static string Shortener(string value, int min, int max, char placeholder) {
        var processed = value.Trim();

        if (processed.Length < min)
            processed = processed.PadRight(min, placeholder);

        if (processed.Length > max)
            processed = processed.Substring(0, max).TrimEnd();

        if (processed.Length < min)
            processed = processed.PadRight(min, placeholder);

        return processed;
    }
}