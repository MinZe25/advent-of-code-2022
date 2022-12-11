using System.Collections;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace AdventOfCode2022;

public static class Day10
{
    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] instructions = input.Split("\n");
        var cycle = 0;
        var x = 1;
        var strength = 0;
        var output = "";

        void RaiseCycle()
        {
            cycle++;
            int pixel = (cycle - 1) % 40;
            if (pixel == 0)
            {
                output += "\n";
            }

            if (pixel == x || pixel == x - 1 || pixel == x + 1)
            {
                output += "█";
            }
            else
            {
                output += " ";
            }

            if (cycle is 20 or 60 or 100 or 140 or 180 or 220)
            {
                strength += cycle * x;
            }
        }

        foreach (string instruction in instructions)
        {
            if (instruction.StartsWith("addx"))
            {
                var quantity = int.Parse(instruction.Split(" ")[1]);
                RaiseCycle();
                RaiseCycle();
                x += quantity;
            }
            else
            {
                RaiseCycle();
            }
        }

        int res1 = strength;
        string? res2 = output;
        Console.WriteLine($"Part 1 Answer: {res1}");
        Console.WriteLine($"Part 2 Answer: \n{res2}");
    }
}