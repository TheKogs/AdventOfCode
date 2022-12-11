namespace AdventOfCode._2022;

public static class Day10
{
	public static void Execute(string input)
	{
		var cycles = new List<int>();
		var spritePos = 1;
		var pos = 0;
		Console.WriteLine("Day10");
		Console.Write("\t");
		input
			.Trim()
			.Split(Environment.NewLine)
			.Select(line => line.Split(" "))
			.Select(instruction => (Cmd: instruction[0], Cnt: instruction.Length > 1 ? int.Parse(instruction[1]) : 0))
			.ToList()
			.ForEach(instruction =>
			{
				switch (instruction.Cmd)
				{
					case "noop":
						cycles.Add(spritePos);
						DrawPixel(pos++, spritePos);
						break;
					case "addx":
						cycles.Add(spritePos);
						DrawPixel(pos++, spritePos);
						cycles.Add(spritePos);
						DrawPixel(pos++, spritePos);

						spritePos += instruction.Cnt;
						break;
				}
			});

		var interestedSignals = new[] { 20, 60, 100, 140, 180, 220 };
		var sumSignalStrength = interestedSignals.Sum(i => cycles[i - 1] * i);
		
		Console.WriteLine($"Sum of signal strength: {sumSignalStrength}");
	}

	private static void DrawPixel(int pos, int spritePos)
	{
		if (pos % 40 >= spritePos - 1 && pos % 40 <= spritePos + 1)
			Console.Write("#");
		else
			Console.Write(" ");
		if (pos % 40 == 39) {Console.Write($"{Environment.NewLine}\t");}
	}
}