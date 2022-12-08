using System.Collections;

namespace AdventOfCode2022;

public static class Day7
{
    private class Node
    {
        public readonly Node? parent;
        public readonly string name;
        public readonly List<Node> children;
        public readonly bool isDirectory;
        private readonly int _size;

        public Node(Node? parent, string name, List<Node> children)
        {
            this.parent = parent;
            this.children = children;
            this.name = name;
            this.isDirectory = true;
            this._size = 0;
        }

        public Node(Node? parent, string name, int size)
        {
            this.parent = parent;
            this.children = new List<Node>();
            this.isDirectory = false;
            this._size = size;
            this.name = name;
        }

        public int size => this.isDirectory ? this.children.Select(c => c.size).Sum() : this._size;
    }

    private static Node _currentNode = new(null, "/", new List<Node>());

    public static void Run()
    {
        string input = Day.GetDayInput();
        var output = input.Split("\n").Skip(1).ToList();
        InstructionHandler(output);
        GoToRoot();
        int res1 = FindAllWithAtMost(_currentNode).Select(x => x.size).Sum();
        int res2 = FindTheSmallestDirectory(_currentNode).size;
        Console.WriteLine($"Part 1 Answer: {res1}");
        Console.WriteLine($"Part 2 Answer: {res2}");
    }

    private static List<Node> FindAllWithAtMost(Node current)
    {
        var rep = new List<Node>();
        if (current.isDirectory && current.size < 100000)
            rep.Add(current);
        rep.AddRange(current.children.SelectMany(FindAllWithAtMost));
        return rep;
    }

    private static Node FindTheSmallestDirectory(Node current)
    {
        int currentSpace = 70000000 - _currentNode.size;
        const int atLeastNeeded = 30000000;
        IEnumerable<Node> possibleSolutions = current.children.Where(c => currentSpace + c.size >= atLeastNeeded);
        IEnumerable<Node> res = possibleSolutions.Select(FindTheSmallestDirectory);
        Node node = res.OrderBy(x => x.size).FirstOrDefault(current);
        return node;
    }

    private static void GoToRoot()
    {
        while (true)
        {
            if (_currentNode.name == "/") return;
            _currentNode = _currentNode.parent!;
        }
    }

    private static void InstructionHandler(IList<string> instructions)
    {
        while (true)
        {
            string instruction = instructions.Pop();
            string[] sp = instruction.Split(" ");

            if (!instruction.StartsWith("$"))
            {
                string type = sp[0];
                string name = sp[1];
                _currentNode.children.Add(type == "dir"
                    //Directory
                    ? new Node(_currentNode, name, new List<Node>())
                    //file
                    : new Node(_currentNode, name, int.Parse(type)));
            }
            else
            {
                if (sp[1] == "cd")
                {
                    _currentNode = sp[2] == ".."
                        ? _currentNode.parent!
                        : _currentNode.children.Find(c => c.name == sp[2])!;
                }
            }

            if (instructions.Count > 0) continue;
            break;
        }
    }

    private static T Pop<T>(this IList<T> instructions)
    {
        T ins = instructions[0];
        instructions.RemoveAt(0);
        return ins;
    }
}