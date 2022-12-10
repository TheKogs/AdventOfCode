namespace AdventOfCode._2022;

public static class Day09
{
	public static void Execute(string input)
	{
		var firstTailPos = new HashSet<(int, int)>();
		var lastTailPos = new HashSet<(int, int)>();
		var queue = Enumerable.Repeat((X: 0, Y: 0), 10).ToArray();
		firstTailPos.Add(queue[1]);
		lastTailPos.Add(queue[9]);
		
		input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Split(" "))
			.Select(instruction => (Direction: instruction[0], Steps: int.Parse(instruction[1])))
			.ToList()
			.ForEach(instruction =>
				Enumerable.Range(0, instruction.Steps).ToList().ForEach(_ =>
				{
					queue[0] = MoveToDirection(queue[0], instruction.Direction);
					Enumerable.Range(1, 9).ToList().ForEach(i => queue[i] = Follow(queue[i - 1], queue[i]));
					firstTailPos.Add(queue[1]);
					lastTailPos.Add(queue[9]);
				}));
		
		Console.WriteLine("Day09");
		Console.WriteLine($"\tFirst Tail position visits: {firstTailPos.Count}");
		Console.WriteLine($"\t9th tail position visits: {lastTailPos.Count}");
	}

	private static (int X, int Y) MoveToDirection((int X, int Y) head, string direction) =>
		direction switch
		{
			"U" => (head.X, head.Y - 1),
			"D" => (head.X, head.Y + 1),
			"L" => (head.X - 1, head.Y),
			"R" => (head.X + 1, head.Y),
			_ => head
		};

	private static (int X, int Y) Follow((int X, int Y) head, (int X, int Y) tail) =>
		Math.Abs(head.X - tail.X) <= 1 && Math.Abs(head.Y - tail.Y) <= 1
			? tail
			: (tail.X + Math.Sign(head.X - tail.X), tail.Y + Math.Sign(head.Y - tail.Y));
}