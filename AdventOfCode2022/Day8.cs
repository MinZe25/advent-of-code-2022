using System.Collections;
using System.Runtime.InteropServices;

namespace AdventOfCode2022;

public static class Day8
{
    public static void Run()
    {
        string input = Day.GetDayInput();
        int[][] grid = input.Split("\n")
            .Select(s => s.ToCharArray().Select(x => x.ToString()).Select(int.Parse).ToArray()).ToArray();
        var res1 = 0;
        int height = grid.Length;
        var res2 = 0;
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                int width = grid[i].Length;
                if (IsVisibleFromOutside(grid, i, j, height - 1, width - 1))
                    res1++;
                int distance = HowManyVisibleFromOutside(grid, i, j, height - 1, width - 1);
                if (distance > res2)
                    res2 = distance;
            }
        }


        Console.WriteLine($"Part 1 Answer: {res1}");
        Console.WriteLine($"Part 2 Answer: {res2}");
    }

    private static bool IsVisibleFromOutside(int[][] grid, int i, int j, int maxI, int maxJ)
    {
        if (i == 0 || j == 0 || i == maxI || j == maxJ) return true;
        return IsVisibleLeft(grid, i, j) ||
               IsVisibleRight(grid, i, j, maxJ) ||
               IsVisibleUp(grid, i, j) ||
               IsVisibleDown(grid, i, j, maxI);
    }

    private static int HowManyVisibleFromOutside(int[][] grid, int i, int j, int maxI, int maxJ)
    {
        return HowManyVisibleLeft(grid, i, j) *
               HowManyVisibleRight(grid, i, j, maxJ) *
               HowManyVisibleUp(grid, i, j) *
               HowManyVisibleDown(grid, i, j, maxI);
    }

    private static bool IsVisibleLeft(int[][] grid, int i, int j)
    {
        int currentValue = grid[i][j];

        for (int k = j - 1; k >= 0; k--)
        {
            if (grid[i][k] >= currentValue) return false;
        }

        return true;
    }

    private static bool IsVisibleRight(int[][] grid, int i, int j, int maxJ)
    {
        int currentValue = grid[i][j];

        for (int k = j + 1; k <= maxJ; k++)
        {
            if (grid[i][k] >= currentValue) return false;
        }

        return true;
    }

    private static bool IsVisibleUp(int[][] grid, int i, int j)
    {
        int currentValue = grid[i][j];

        for (int k = i - 1; k >= 0; k--)
        {
            if (grid[k][j] >= currentValue) return false;
        }

        return true;
    }

    private static bool IsVisibleDown(int[][] grid, int i, int j, int maxI)
    {
        int currentValue = grid[i][j];

        for (int k = i + 1; k <= maxI; k++)
        {
            if (grid[k][j] >= currentValue) return false;
        }

        return true;
    }

    private static int HowManyVisibleLeft(int[][] grid, int i, int j)
    {
        int currentValue = grid[i][j];
        var hm = 0;
        for (int k = j - 1; k >= 0; k--)
        {
            hm++;
            if (grid[i][k] >= currentValue) break;
        }

        return hm;
    }

    private static int HowManyVisibleRight(int[][] grid, int i, int j, int maxJ)
    {
        int currentValue = grid[i][j];
        var hm = 0;
        for (int k = j + 1; k <= maxJ; k++)
        {
            hm++;
            if (grid[i][k] >= currentValue) break;
        }

        return hm;
    }

    private static int HowManyVisibleUp(int[][] grid, int i, int j)
    {
        int currentValue = grid[i][j];
        var hm = 0;
        for (int k = i - 1; k >= 0; k--)
        {
            hm++;
            if (grid[k][j] >= currentValue) break;
        }

        return hm;
    }

    private static int HowManyVisibleDown(int[][] grid, int i, int j, int maxI)
    {
        int currentValue = grid[i][j];
        var hm = 0;
        for (int k = i + 1; k <= maxI; k++)
        {
            hm++;
            if (grid[k][j] >= currentValue) break;
        }

        return hm;
    }
}