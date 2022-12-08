namespace AdventOfCode._2022;

public static class Day03
{
	public static void Execute(string input)
	{
		var commonItemsPrioritySum = input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line =>
				GetItemValue(
					line[..(line.Length / 2)]
						.Intersect(line[(line.Length / 2)..])
						.First()))
			.Sum();

		var stickersPrioritySum = input
			.Trim()
			.Split(Environment.NewLine)
			.Chunk(3)
			.Select(group =>
				GetItemValue(group
					.Aggregate<IEnumerable<char>>((previous, next) => 
						previous.Intersect(next))
					.First()))
			.Sum();
		
		Console.WriteLine("Day03");
		Console.WriteLine($"\tPriority Sum Part 1: {commonItemsPrioritySum}");
		Console.WriteLine($"\tPriority Sum Part 2: {stickersPrioritySum}");
	}

	private static int GetItemValue(char item) =>
		item switch
		{
			>= 'a' and <= 'z' => item - 'a' + 1,
			>= 'A' and <= 'Z' => item - 'A' + 26 + 1,
			_ => 0
		};
}