using System.Collections;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022;

public static class Day3
{
    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] trucks = input.Split("\n");
        int res1 = trucks.Select(x =>
        {
            int l = x.Length / 2;
            string x1 = x[..l];
            string x2 = x[l..];
            return FindRepeated(x1, x2).Points();
        }).Sum();
        var groups = new List<string[]>();
        var chunkSize = 3;
        for (var i = 0; i < trucks.Length; i += chunkSize)
        {
            string[] chunk = trucks[i..(i + chunkSize)];
            groups.Add(chunk);
        }

        int res2 = groups.Select(x => FindRepeated2(x[0], x[1], x[2]).Points()).Sum();
        Console.WriteLine($"First problem: {res1}");
        Console.WriteLine($"Second problem: {res2}");
    }

    private static char FindRepeated(string t1, string t2)
    {
        return t2.Select(i => i).FirstOrDefault(t1.Contains);
    }

    private static char FindRepeated2(string t1, string t2, string t3)
    {
        return FindLongest(t1, t2, t3)
            .Select(i => i)
            .FirstOrDefault(c => t1.Contains(c) && t2.Contains(c) && t3.Contains(c));
    }

    private static int Points(this char c)
    {
        return c >= 97 ? c - 96 : c - 65 + 27;
    }

    private static string FindLongest(string t1, string t2, string t3)
    {
        int l1 = t1.Length;
        int l2 = t2.Length;
        int l3 = t3.Length;
        if (l1 >= l2 && l1 >= l3) return t1;
        if (l2 >= l1 && l2 >= l3) return t2;
        return t3;
    }
}