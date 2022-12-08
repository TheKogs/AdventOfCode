using System.Text.RegularExpressions;

namespace AdventOfCode._2022;

public static class MyExtensions
{
    public static IEnumerable<T> FlattenRecursive<T>(this IEnumerable<T> nodes, Func<T, IEnumerable<T>> selector)
    {
        var selectRecursive = nodes as T[] ?? nodes.ToArray();
        if (selectRecursive.Any() == false)
        {
            return selectRecursive; 
        }

        var descendants = selectRecursive
            .SelectMany(selector)
            .FlattenRecursive(selector);

        return selectRecursive.Concat(descendants);
    } 
}
public record Node(Node? Parent, List<Node> Directories)
{
    public int FileSizes { get; set; }
    public int Size { get; set; }
};

public static class Day07
{
    public static void Execute(string input)
    {
        var rxCd = new Regex(@"\$\s+cd\s+(?<directory>.*)");
        var rxFile = new Regex(@"(?<filesize>\d+)\s+(?<filename>.*)");
        var rootNode = new Node(null, new List<Node>());
        var currentNode = rootNode;
        input
            .Trim()
            .Split(Environment.NewLine)
            .ToList()
            .ForEach(line =>
                {
                    if (rxCd.IsMatch(line))
                    {
                        switch (rxCd.Match(line).Groups["directory"].Value)
                        {
                            case "/":
                                currentNode = rootNode;
                                return;
                            case "..":
                                currentNode = currentNode.Parent;
                                return;
                            default:
                            {
                                var newNode = new Node(currentNode, new List<Node>());
                                currentNode.Directories.Add(newNode);
                                currentNode = newNode;
                                return;
                            }
                        }
                    }
                    if (!rxFile.IsMatch(line)) return;
                    currentNode.FileSizes += int.Parse(rxFile.Match(line).Groups["filesize"].Value);
                });
        
        CalcDirectorySizes(rootNode);

        var totalSize = rootNode.Directories
            .FlattenRecursive(x => x.Directories)
            .Where(x => x.Size <= 100000)
            .Sum(x => x.Size);

        var freeUpSize = rootNode.Directories
            .FlattenRecursive(x => x.Directories)
            .Where(x => x.Size >= rootNode.Size - 40000000)
            .Min(x => x.Size);

        Console.WriteLine("Day07");
        Console.WriteLine($"\tTotal Size: {totalSize}");
        Console.WriteLine($"\tFree Up Size: {freeUpSize}");
    }

    private static void CalcDirectorySizes(Node node)
    {
        node.Directories.ForEach(CalcDirectorySizes);
        node.Size = node.Directories.Sum(x => x.Size) + node.FileSizes;
    }
}