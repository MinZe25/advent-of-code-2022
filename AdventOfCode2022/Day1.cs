namespace AdventOfCode2022;

public static class Day1
{
    public static void Run()
    {
        string input = 1.GetInputForDay();
        string[] allElves = input.Split("\n\n");
        int[] allCals = allElves
            .Select(elf =>
                elf.Split("\n")
                    .Select(int.Parse)
                    .Sum())
            .OrderDescending().ToArray();
        int f1 = allCals[0];
        int f2 = allCals[1];
        int f3 = allCals[2];
        Console.WriteLine($"1st problem: {f1}");
        Console.WriteLine($"2nd problem: {f1 + f2 + f3}");
    }
}