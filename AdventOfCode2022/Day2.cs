namespace AdventOfCode2022;

public static class Day2
{
    private static int WinPoints = 6;
    private static int DrawPoints = 3;
    private static int LoosePoints = 0;

    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] rounds = input.Split("\n");
        int res1 = rounds.Select(r => CalculateRound(r[0], r[2])).Sum();
        int res2 = rounds.Select(r => CalculateSecondPart(r[0], r[2])).Sum();
        Console.WriteLine($"First problem: {res1}");
        Console.WriteLine($"Second problem: {res2}");
    }

    private static int CalculateRound(char p1, char p2)
    {
        char p2T = p2.ToFirst();
        int points = p2T.Points();
        if (p2T == p1)
            return DrawPoints + points;
        if (p2T.IsWin(p1))
            return WinPoints + points;
        return LoosePoints + points;
    }

    private static int CalculateSecondPart(char p1, char p2)
    {
        char p2T = p2 switch
        {
            'Y' => p1.ToSecond(),
            'X' => p1.ToWin().ToSecond(),
            _ => p1.ToLost().ToSecond()
        };
        return CalculateRound(p1, p2T);
    }


    private static int Points(this char letter)
    {
        return letter - 64;
    }

    private static char ToFirst(this char letter)
    {
        return (char)(letter - 23);
    }

    private static char ToSecond(this char letter)
    {
        return (char)(letter + 23);
    }

    private static bool IsDraw(this char letter, char enemy)
    {
        return letter == enemy;
    }

    private static bool IsLost(this char letter, char enemy)
    {
        return !letter.IsDraw(enemy) && ((letter - 64) % 3) + 65 == enemy;
    }

    private static bool IsWin(this char letter, char enemy)
    {
        return !letter.IsDraw(enemy) && !letter.IsLost(enemy);
    }

    private static char ToLost(this char letter)
    {
        return (char)(((letter - 64) % 3) + 65);
    }

    private static char ToWin(this char letter)
    {
        return (char)(((letter - 63) % 3) + 65);
    }
}