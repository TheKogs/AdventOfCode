namespace AdventOfCode._2022;

public static class Day04
{
	public static void Execute(string input)
	{
		var sections = input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Split(',', '-').Select(int.Parse).ToList())
			.Select(s => (Elv1Sections: Enumerable.Range(s[0], s[1] - s[0] + 1), Elv2Sections: Enumerable.Range(s[2], s[3] - s[2] + 1)))
			.ToList();

		var fullOverlap = sections 
			.Select(s => (IntersectCount: s.Elv1Sections.Intersect(s.Elv2Sections).Count(), Elv1Count: s.Item1.Count(), Elv2Count: s.Item2.Count()))
			.Select(s => s.IntersectCount == s.Elv1Count || s.IntersectCount == s.Elv2Count ? 1 : 0)
			.Sum();
		
		var partialOverlap = sections
			.Select(s=> s.Elv1Sections.Intersect(s.Elv2Sections).Any() ? 1 : 0)
			.Sum();
		
		Console.WriteLine("Day04");
		Console.WriteLine($"\tFull overlap sections: {fullOverlap}");
		Console.WriteLine($"\tPartial overlap sections: {partialOverlap}");
	}

}