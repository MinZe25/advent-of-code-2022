using System.Reflection;

namespace AdventOfCode2022;

public static class Day01
{
    public static void Run()
    {
        // string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
        string lines = File.ReadAllText(@"01.txt").Replace("\r", "");
        string[] allElves = lines.Split("\n\n");
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
        Console.WriteLine($"2nd problem: {f1+f2+f3}");
    }
}