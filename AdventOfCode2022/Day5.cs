using System.Collections;
using System.Collections.Immutable;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class Day5
{
    public static void Run()
    {
        string input = Day.GetDayInput();
        string[][] sep = input.Split("\n\n").Select(x => x.Split("\n")).ToArray();
        string[] iss = sep[0].SkipLast(1).ToArray();
        string[] movements = sep[1];

        var crates1 = new Dictionary<int, List<string>>();
        var crates2 = new Dictionary<int, List<string>>();
        const int chunkSize = 4;
        foreach (string x in iss)
        {
            for (var i = 0; i < x.Length; i += chunkSize)
            {
                string chunk = x[i..(i + chunkSize - 1)];
                string num = chunk
                    .Replace(" ", "")
                    .Replace("[", "")
                    .Replace("]", "");
                if (num == "") continue;
                int ind = (i / 4) + 1;
                if (!crates1.ContainsKey(ind))
                {
                    crates1.Add(ind, new List<string>());
                }

                crates1[ind].Add(num);
                if (!crates2.ContainsKey(ind))
                {
                    crates2.Add(ind, new List<string>());
                }

                crates2[ind].Add(num);
            }
        }

        var pattern = new Regex(@"move (?<hm>[0-9]*) from (?<origin>[0-9]*) to (?<goal>[0-9]*)");

        foreach (string x in movements)
        {
            Match m = pattern.Match(x);
            var hm = int.Parse(m.Groups["hm"].Value);
            var origin = int.Parse(m.Groups["origin"].Value);
            var goal = int.Parse(m.Groups["goal"].Value);

            List<string> c1 = crates1[origin];
            List<string> nC = c1.Splice(0, hm);
            nC.Reverse();
            crates1[goal].InsertRange(0, nC);


            List<string> c2 = crates2[origin];
            List<string> nC2 = c2.Splice(0, hm);
            crates2[goal].InsertRange(0, nC2);
        }

        var res1 = "";
        var res2 = "";
        for (var i = 1; i < 10; i++)
        {
            res1 += crates1[i].First();
            res2 += crates2[i].First();
        }
        Console.WriteLine($"First problem: {res1}");
        Console.WriteLine($"Second problem: {res2}");
    }

    private static List<T> Splice<T>(this List<T> source, int index, int count)
    {
        List<T> items = source.GetRange(index, count);
        source.RemoveRange(index, count);
        return items;
    }
}