namespace AdventOfCode2022;

public static class Day
{
    public static string GetInputForDay(this int day){
        return File.ReadAllText(@$"{day}.txt").Replace("\r", "");
    }
}