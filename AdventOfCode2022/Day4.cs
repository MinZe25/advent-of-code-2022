using System.Collections;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2022;

public static class Day4
{
    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] pairs = input.Split("\n");
        int res1 = pairs.Select(x =>
        {
            string[] xs = x.Split(",");
            int[][] rs = xs.Select(s => s.Split("-").Select(int.Parse).ToArray()).ToArray();
            int r1 = rs[0][0];
            int r2 = rs[0][1];
            int r3 = rs[1][0];
            int r4 = rs[1][1];
            if (r1 >= r3 && r2 <= r4) return 1;
            if (r3 >= r1 && r4 <= r2) return 1;
            return 0;
        }).Sum();

        int res2 = pairs.Select(x =>
        {
            string[] xs = x.Split(",");
            int[][] rs = xs.Select(s => s.Split("-").Select(int.Parse).ToArray()).ToArray();
            int r1 = rs[0][0];
            int r2 = rs[0][1];
            int r3 = rs[1][0];
            int r4 = rs[1][1];
            if (r3 < r1)
                return r4 >= r1 ? 1 : 0;
            return r3 <= r2 ? 1 : 0;
        }).Sum();

        Console.WriteLine($"First problem: {res1}");
        Console.WriteLine($"Second problem: {res2}");
    }
}