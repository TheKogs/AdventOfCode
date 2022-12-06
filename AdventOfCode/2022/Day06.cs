namespace AdventOfCode._2022;

public static class Day06
{
	public static void Execute(string input)
	{
		var sequence = input.ToCharArray();

		var startOfPacket = CountElementsTillMarker(sequence, 4);
		var startOfMessage = CountElementsTillMarker(sequence, 14);
		
		Console.WriteLine("Day06");
		Console.WriteLine($"\tStart of Packet Index: {startOfPacket}");
		Console.WriteLine($"\tStart of Message Index: {startOfMessage}");
	}

	private static int CountElementsTillMarker(IReadOnlyCollection<char> sequence, int markerSize) =>
		( from i in Enumerable.Range(0, sequence.Count - markerSize)
		   let cntDuplicates = sequence
				   .Skip(i)
				   .Take(markerSize)
				   .GroupBy(x => x)
				   .Count(g => g.Count() > 1)
		 where cntDuplicates == 0
		select i + markerSize)
		.FirstOrDefault();
	
}