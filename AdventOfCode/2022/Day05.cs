using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022;

public static class Day05
{
	public static void Execute(string input)
	{
		var split = input.Split($"{Environment.NewLine}{Environment.NewLine}");
		var stacksInput = split[0];
		var instructionsInput = split[1].Trim();
		List<Stack<char>> stacksPart1 = new();

		stacksInput
			.Split(Environment.NewLine)
			.Select(line => line + " ") // Last element is only 3 chars, add a space to be able to split by 4 chars
			.Reverse()
			.Select(line =>
				Enumerable
					.Range(0, line.Length / 4)
					.Select((index, stackNumber) => (StackNumber: stackNumber, Box: line.Substring(index * 4, 4).Trim()))
					.ToList()
			)
			.ToList()
			.ForEach(layer =>
				layer.ForEach(element =>
				{
					if (int.TryParse(element.Box, out _)) stacksPart1.Add(new Stack<char>());
					else if (!string.IsNullOrEmpty(element.Box)) stacksPart1[element.StackNumber].Push(element.Box[1]);
				})
			);

		// copy stacks list
		var stacksPart2 = stacksPart1.Select(x => new Stack<char>(x.Reverse())).ToList();
		
		var regex = new Regex(@"move\s+(?<count>\d+)\s+from\s+(?<from>\d+)\s+to\s+(?<to>\d+)");
		var parsedInstructions = instructionsInput
			.Split(Environment.NewLine)
			.Select(instruction => regex.Match(instruction).Groups)
			.Select(instruction => (
				Count: int.Parse(instruction["count"].Value), 
				From: int.Parse(instruction["from"].Value) - 1, 
				To: int.Parse(instruction["to"].Value) - 1))
			.ToList();
		
		parsedInstructions
			.ForEach(instruction =>
				Enumerable.Range(0, instruction.Count).ToList().ForEach(_ => 
					stacksPart1[instruction.To].Push(
						stacksPart1[instruction.From].Pop())
			));

		parsedInstructions
			.ForEach(instruction =>
			{
				Stack<char> temp = new();
				Enumerable.Range(0, instruction.Count).ToList().ForEach(_ =>
					temp.Push(stacksPart2[instruction.From].Pop()));
				Enumerable.Range(0, temp.Count).ToList().ForEach(_ =>
					stacksPart2[instruction.To].Push(temp.Pop()));
			});

		var resultPart1 = new StringBuilder();
		stacksPart1.ForEach(stack => resultPart1.Append(stack.Pop()));
		var resultPart2 = new StringBuilder();
		stacksPart2.ForEach(stack => resultPart2.Append(stack.Pop()));
		
		Console.WriteLine("Day05");
		Console.WriteLine($"\tTop boxes part 1: {resultPart1}");
		Console.WriteLine($"\tTop boxes part 2: {resultPart2}");
	}
}