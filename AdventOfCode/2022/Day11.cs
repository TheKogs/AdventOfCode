namespace AdventOfCode._2022;

public record Monkey(List<long> Items, Func<long, long> Operation, int DivisibleBy, int ThrowToIfTrue, int ThrowToIfFalse)
{
	public long InspectionCnt { get; set; }		// need to be mutable, therefore define here
};

public static class Day11
{
	public static void Execute(string input)
	{
		var monkeysPart1 = input
			.Trim()
			.Split($"{Environment.NewLine}{Environment.NewLine}")
			.Select(monkey => monkey.Split(Environment.NewLine))
			.Select(monkey =>
			{
				var items = monkey[1].Trim()
					.Split(":")[1].Trim()
					.Split(",").Select(long.Parse);
				var operation = monkey[2].Trim()
					.Split("=")[1].Trim()
					.Split(" ");
				Func<long, long> op = operation[1] switch
				{
					"+" when int.TryParse(operation[2], out var num) => x => x + num,
					"+" => x => x + x,
					"*" when int.TryParse(operation[2], out var num) => x => x * num,
					"*" => x => x * x,
					_ => throw new Exception("Unknown operation")
				};
				var divisibleBy = int.Parse(monkey[3].Trim().Split("by")[1]);
				var throwToIfTrue = int.Parse(monkey[4].Trim().Split("monkey")[1]);
				var throwToIfFalse = int.Parse(monkey[5].Trim().Split("monkey")[1]);

				return new Monkey(items.ToList(), op, divisibleBy, throwToIfTrue, throwToIfFalse);
			}).ToList();

		// clone monkey input as it will change in part 1, but unchanged data is needed for part 2
		var monkeysPart2 = monkeysPart1.Select(monkey => monkey with { Items = monkey.Items.ToList() }).ToList();

		CalcMonkeyBusiness(monkeysPart1, 20, x => x / 3);
		var resultPart1 = monkeysPart1
			.Select(monkey => monkey.InspectionCnt)
			.OrderByDescending(cnt => cnt)
			.Take(2)
			.Aggregate((m1, m2) => m1 * m2);

		var kgv = monkeysPart2.Aggregate(1, (kgv, monkey) => kgv * monkey.DivisibleBy);
		CalcMonkeyBusiness(monkeysPart2, 10000, x => x % kgv);
		var resultPart2 =  monkeysPart2
			.OrderByDescending(monkey => monkey.InspectionCnt)
			.Take(2)
			.Select(monkey => monkey.InspectionCnt)
			.Aggregate((m1, m2) => m1 * m2);
		
		Console.WriteLine("Day11");
		Console.WriteLine($"\tLevel of monkey business Part 1: {resultPart1}");
		Console.WriteLine($"\tLevel of monkey business Part 2: {resultPart2}");
	}

	private static void CalcMonkeyBusiness(List<Monkey> monkeys, int rounds, Func<long,long> worryReducer) =>
		Enumerable.Range(1, rounds).ToList().ForEach(_ =>
			monkeys.ForEach(monkey =>
				monkey.Items.ToList().ForEach(item =>
				{
					var worried = worryReducer(monkey.Operation(item));
					monkeys[worried % monkey.DivisibleBy == 0 ? monkey.ThrowToIfTrue : monkey.ThrowToIfFalse].Items.Add(worried);
					monkey.InspectionCnt++;
					monkey.Items.Remove(item);
				})
			)
		);
}