namespace AdventOfCode._2022;

public static class Day08
{
	public static void Execute(string input)
	{
		var treeMap = input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Select(x => int.Parse(x.ToString())).ToArray())
			.ToArray();

		var visibleTrees = new HashSet<(int, int)>();
		var (sizeY, sizeX) = (treeMap.Length, treeMap[0].Length);
		
		// todo refactor ugly repeating code
		for (var y = 0; y < sizeY; y++)
		{
			for (int x = 0, highestTree = -1; x < sizeX; x++)
			{
				if (treeMap[y][x] <= highestTree) continue;
				highestTree = treeMap[y][x];
				visibleTrees.Add((x, y));
			}
			for (int x = sizeX-1, highestTree = -1; x >= 0; x--)
			{
				if (treeMap[y][x] <= highestTree) continue;
				highestTree = treeMap[y][x];
				visibleTrees.Add((x, y));
			}
		}

		for (var x = 0; x < sizeX; x++)
		{
			for (int y = 0, highestTree = -1; y < sizeY; y++)
			{
				if (treeMap[y][x] <= highestTree) continue;
				highestTree = treeMap[y][x];
				visibleTrees.Add((x, y));
			}
			for (int y = sizeY-1, highestTree = -1; y >= 0; y--)
			{
				if (treeMap[y][x] <= highestTree) continue;
				highestTree = treeMap[y][x];
				visibleTrees.Add((x, y));
			}
		}
	    
		var highestScenicScore = (
				from x in Enumerable.Range(0, sizeX)
				from y in Enumerable.Range(0, sizeY)
				select CheckNorth(treeMap, x, y, sizeY) * CheckSouth(treeMap, x, y, sizeY) *
				       CheckWest(treeMap, x, y) * CheckEast(treeMap, x, y, sizeX))
			.Max();
			       
		Console.WriteLine("Day08");
		Console.WriteLine($"\tVisible trees from outside: {visibleTrees.Count}");
		Console.WriteLine($"\tHighest scenic score: {highestScenicScore}");
	}

	// todo refactor ugly repeating methods
	private static int CheckNorth(int[][] treeMap, int x, int y, int sizeY)
	{
		var cnt = 0;
		for (var yy = y - 1; yy >= 0; yy--)
		{
			cnt++;
			if (treeMap[y][x] <= treeMap[yy][x]) break;
		}
		return cnt;
	}
	private static int CheckSouth(int[][] treeMap, int x, int y, int sizeY)
	{
		var cnt = 0;
		for (var yy = y + 1; yy < sizeY; yy++)
		{
			cnt++;
			if (treeMap[y][x] <= treeMap[yy][x]) break;
		}
		return cnt;
	}
	private static int CheckEast(int[][] treeMap, int x, int y, int sizeX)
	{
		var cnt = 0;
		for (var xx = x + 1; xx < sizeX; xx++)
		{
			cnt++;
			if (treeMap[y][x] <= treeMap[y][xx]) break;
		}
		return cnt;
	}
	private static int CheckWest(int[][] treeMap, int x, int y)
	{
		var cnt = 0;
		for (var xx = x-1; xx >= 0; xx--)
		{
			cnt++;
			if (treeMap[y][x] <= treeMap[y][xx]) break;
		}
		return cnt;
	}
}