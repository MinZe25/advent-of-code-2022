using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode2022;

public static class Day
{
    public static string GetDayInput()
    {
        MethodBase mth = new StackTrace().GetFrame(1)!.GetMethod()!;
        string cls = mth.ReflectedType!.Name;
        var day = int.Parse(cls.Replace("Day", ""));
        return File.ReadAllText(@$"inputs/{day}.txt").Replace("\r", "");
    }
}