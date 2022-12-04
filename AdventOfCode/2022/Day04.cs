namespace AdventOfCode._2022;

public static class Day04
{
	public static void Execute(string input)
	{
		var sections = input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Split(',', '-').Select(int.Parse).ToList())
			.Select(s => (Enumerable.Range(s[0], s[1] - s[0] + 1), Enumerable.Range(s[2], s[3] - s[2] + 1)))
			.ToList();

		var fullOverlap = sections 
			.Select(s => (s.Item1.Intersect(s.Item2).Count(), s.Item1.Count(), s.Item2.Count()))
			.Select(s => s.Item1 == s.Item2 || s.Item1 == s.Item3 ? 1 : 0)
			.Sum();
		
		var partialOverlap = sections
			.Select(s=> s.Item1.Intersect(s.Item2).Any() ? 1 : 0)
			.Sum();
		
		Console.WriteLine("Day04");
		Console.WriteLine($"\tFull overlap sections: {fullOverlap}");
		Console.WriteLine($"\tPartial overlap sections: {partialOverlap}");
	}

}