namespace AdventOfCode2022;

public abstract class Day
{
    protected string input;

    protected Day(int currentDay)
    {
        this.input = File.ReadAllText(@$"{currentDay}.txt").Replace("\r", "");
    }
}