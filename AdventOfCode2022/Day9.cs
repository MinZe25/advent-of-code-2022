using System.Collections;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace AdventOfCode2022;

public static class Day9
{
    private class Coord
    {
        public readonly int x;
        public readonly int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Coord c && Equals(c);
        }

        private bool Equals(Coord other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.x, this.y);
        }

        public bool InRange(Coord other)
        {
            return Math.Abs(other.x - this.x) <= 1 && Math.Abs(other.y - this.y) <= 1;
        }
    }

    private class Node
    {
        private readonly Node? _child;
        public Coord coord;

        public Node(Node? child, Coord coord)
        {
            this._child = child;
            this.coord = coord;
        }

        public void Move(Coord newCoord)
        {
            this.coord = newCoord;
            if (this._child is null) return;
            if (this._child.coord.InRange(this.coord)) return;
            // Move the child
            // Know to which coord do I need to move
            if (this._child.coord.y == this.coord.y)
            {
                // Move to the left or right
                this._child.Move(this.coord.x > this._child.coord.x
                    ? new Coord(this.coord.x - 1, this.coord.y)
                    : new Coord(this.coord.x + 1, this.coord.y));
            }
            else if (this._child.coord.x == this.coord.x)
            {
                // Move up or down
                this._child.Move(this.coord.y > this._child.coord.y
                    ? new Coord(this.coord.x, this.coord.y - 1)
                    : new Coord(this.coord.x, this.coord.y + 1));
            }
            else
            {
                // Diagonally
                int newY = this.coord.y > this._child.coord.y ? 1 : -1;
                int newX = this.coord.x > this._child.coord.x ? 1 : -1;
                this._child.Move(new Coord(this._child.coord.x + newX, this._child.coord.y + newY));
            }
        }
    }

    public static void Run()
    {
        string input = Day.GetDayInput();
        string[] instructions = input.Split("\n");

        var tailNode = new Node(null, new Coord(0, 0));
        var headNode = new Node(tailNode, new Coord(0, 0));
        int res1 = GetPositionsOfTheTail(instructions, headNode, tailNode).Count;
        tailNode = new Node(null, new Coord(0, 0));
        Node previous = tailNode;
        for (var i = 0; i < 8; i++)
        {
            var newNode = new Node(previous, new Coord(0, 0));
            previous = newNode;
        }

        headNode = new Node(previous, new Coord(0, 0));
        int res2 = GetPositionsOfTheTail(instructions, headNode, tailNode).Count;
        Console.WriteLine($"Part 1 Answer: {res1}");
        Console.WriteLine($"Part 2 Answer: {res2}");
    }

    private static HashSet<Coord> GetPositionsOfTheTail(IEnumerable<string> instructions, Node headNode, Node tailNode)
    {
        var positionsOfTheTail = new HashSet<Coord>();
        foreach (string instruction in instructions)
        {
            string[] sp = instruction.Split(" ");
            string dir = sp[0];
            var amount = int.Parse(sp[1]);
            switch (dir)
            {
                case "U":
                    for (var i = 0; i < amount; i++)
                    {
                        headNode.Move(new Coord(headNode.coord.x, headNode.coord.y + 1));
                        positionsOfTheTail.Add(tailNode.coord);
                    }

                    break;
                case "D":
                    for (var i = 0; i < amount; i++)
                    {
                        headNode.Move(new Coord(headNode.coord.x, headNode.coord.y - 1));
                        positionsOfTheTail.Add(tailNode.coord);
                    }

                    break;
                case "L":
                    for (var i = 0; i < amount; i++)
                    {
                        headNode.Move(new Coord(headNode.coord.x - 1, headNode.coord.y));
                        positionsOfTheTail.Add(tailNode.coord);
                    }

                    break;
                case "R":
                    for (var i = 0; i < amount; i++)
                    {
                        headNode.Move(new Coord(headNode.coord.x + 1, headNode.coord.y));
                        positionsOfTheTail.Add(tailNode.coord);
                    }

                    break;
            }
        }

        return positionsOfTheTail;
    }
}