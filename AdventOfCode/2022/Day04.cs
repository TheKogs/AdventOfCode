namespace AdventOfCode._2022;

public static class Day04
{
	public static void Execute(string input)
	{
		var sectionsInput = input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Split(',', '-').Select(int.Parse).ToList())
			.Select(s => (Elv1: Enumerable.Range(s[0], s[1] - s[0] + 1), Elv2: Enumerable.Range(s[2], s[3] - s[2] + 1)))
			.ToList();

		var fullOverlap = sectionsInput
			.Select(sections => (
				IntersectCount: sections.Elv1.Intersect(sections.Elv2).Count(), 
				SmallerSectionCount: Math.Min(sections.Item1.Count(), sections.Item2.Count())))
			.Count(s => s.IntersectCount == s.SmallerSectionCount);

		var partialOverlap = sectionsInput
			.Count(sections => sections.Elv1.Intersect(sections.Elv2).Any());
		
		Console.WriteLine("Day04");
		Console.WriteLine($"\tFull overlap sections: {fullOverlap}");
		Console.WriteLine($"\tPartial overlap sections: {partialOverlap}");
	}

}