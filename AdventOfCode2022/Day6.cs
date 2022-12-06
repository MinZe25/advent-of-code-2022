namespace AdventOfCode2022;

public static class Day6
{
    public static void Run()
    {
        string input = Day.GetDayInput();

        Console.WriteLine($"Part 1 Answer: {FindMessage(input, 4)}");
        Console.WriteLine($"Part 2 Answer: {FindMessage(input, 14)}");
    }

    private static int FindMessage(string input, int length)
    {
        for (var i = 0; i < input.Length; i++)
            if (input.Substring(i, length).ToCharArray().ToHashSet().Count == length)
                return i + length;
        return -1;
    }
}