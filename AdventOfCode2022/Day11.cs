using System.Text.RegularExpressions;

namespace AdventOfCode2022;

public static class Day11
{
    private class Monkey
    {
        private readonly List<long> _items;
        private readonly List<Monkey> _allMonkeys;
        private readonly Func<long, long> _operationAction;
        public readonly long testDivValue;
        private readonly long _trueMonkey;
        private readonly long _falseMonkey;
        public long itemsInspected;
        private readonly long _divisor;
        public static long mod;

        public Monkey(List<long> items, Func<long, long> operationAction, long testDivValue, long trueMonkey,
            long falseMonkey, ref List<Monkey> allMonkeys, long divisor)
        {
            this._items = items;
            this._operationAction = operationAction;
            this.testDivValue = testDivValue;
            this._trueMonkey = trueMonkey;
            this._falseMonkey = falseMonkey;
            this._allMonkeys = allMonkeys;
            this._divisor = divisor;
        }

        public void Run()
        {
            foreach (int item in this._items)
            {
                this.itemsInspected++;
                long newItem = this._operationAction.Invoke(item);
                newItem = newItem / this._divisor % mod;
                var monkeyIndex = (int)(newItem % this.testDivValue == 0 ? this._trueMonkey : this._falseMonkey);
                this._allMonkeys[monkeyIndex]._items.Add(newItem);
            }

            this._items.Clear();
        }
    }

    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] monkeys = input.Split("\n\n");

        long res1 = RunRounds(SetAllMonkeys(monkeys, 3), 20);
        long res2 = RunRounds(SetAllMonkeys(monkeys, 1), 10_000);

        Console.WriteLine($"Part 1 Answer: {res1}");
        Console.WriteLine($"Part 2 Answer: {res2}");
    }

    private static long RunRounds(List<Monkey> allMonkeys, int rounds)
    {
        for (var i = 0; i < rounds; i++)
            allMonkeys.ForEach(m => m.Run());
        return allMonkeys.Select(m => m.itemsInspected).OrderDescending().Take(2).Aggregate((i, m) => i * m);
    }

    private static List<Monkey> SetAllMonkeys(string[] monkeys, int divide)
    {
        var pattern = new Regex(
            @"Monkey (?<index>[0-9]):\n  Starting items: (?<items>.*)\n  Operation: new =" +
            @" old (?<op>.) (?<opValue>.*)\n  Test: divisible by (?<testDivVal>[0-9]*)\n" +
            @"    If true: throw to monkey (?<trueMonkey>[0-9])\n    If false: throw to monkey (?<falseMonkey>[0-9])");

        var allMonkeys = new List<Monkey>();
        foreach (string monkeyLine in monkeys)
        {
            Match m = pattern.Match(monkeyLine);
            string op = m.Groups["op"].Value;
            var items = m.Groups["items"].Value.Split(",").Select(long.Parse).ToList();
            string opValue = m.Groups["opValue"].Value;
            var testDivVal = int.Parse(m.Groups["testDivVal"].Value);
            var trueMonkey = int.Parse(m.Groups["trueMonkey"].Value);
            var falseMonkey = int.Parse(m.Groups["falseMonkey"].Value);
            allMonkeys.Add(new Monkey(items, i =>
                {
                    long retVal = opValue == "old" ? i : int.Parse(opValue);
                    return op == "+" ? i + retVal : i * retVal;
                }, testDivVal, trueMonkey, falseMonkey, ref allMonkeys, divide)
            );
        }

        Monkey.mod = allMonkeys.Select(m => m.testDivValue).Aggregate((i, i1) => i * i1);
        //I simply don't understand the reasoning behind the LCM.
        //I had to google the reasoning for the second part (for the first part it's not needed)
        return allMonkeys;
    }
}