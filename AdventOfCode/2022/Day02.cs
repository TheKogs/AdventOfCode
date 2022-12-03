namespace AdventOfCode._2022;

public static class Extensions {
	public static void Deconstruct<T>(this IList<T> list, out T first, out T second) {
		first = list.Count > 0 ? list[0] : default(T); // or throw
		second = list.Count > 1 ? list[1] : default(T); // or throw
	}
}
public static class Day02
{
	// Result Part 1: 8933
	// Result Part 2: 11998
	private enum HandSign
	{
		Rock,
		Paper,
		Scissor
	};

	private enum Result
	{
		Loose,
		Draw,
		Win
	};

	public static void Execute(string input)
	{
		var allRounds = input
			.Trim()
			.Split(Environment.NewLine);
		
		var totalScorePart1 = allRounds
			.Select(round =>
			{
				var (opponentHand, myHand) = round
					.Trim()
					.Split(" ")
					.Select(ToHandSign)
					.ToList();
				return CalcTotalScore(opponentHand, myHand);
			}).Sum();
		
		var totalScorePart2 = allRounds
			.Select(round =>
			{
				var (hand, result) = round
					.Trim()
					.Split(" "); 
				return CalcTotalScore(
					ToHandSign(hand), 
					CalcMove(ToHandSign(hand), ToExpectedResult(result)));
			}).Sum();

		Console.WriteLine("Day02");
		Console.WriteLine($"\tPoints Part 1: {totalScorePart1}");
		Console.WriteLine($"\tPoints Part 2: {totalScorePart2}");
	}

	private static HandSign CalcMove(HandSign handSign, Result expectedResult) =>
		(handSign, expectedResult) switch
		{
			(HandSign.Rock, Result.Draw) or (HandSign.Paper, Result.Loose) or (HandSign.Scissor, Result.Win) => HandSign.Rock,
			(HandSign.Rock, Result.Loose) or (HandSign.Paper, Result.Win) or (HandSign.Scissor, Result.Draw) => HandSign.Scissor,
			(HandSign.Rock, Result.Win) or (HandSign.Paper, Result.Draw) or (HandSign.Scissor, Result.Loose) => HandSign.Paper,
			_ => HandSign.Paper		// default should never be reached
		};

	private static int CalcTotalScore(HandSign opponent, HandSign myself) => HandSignScore(myself) + HandResultScore(opponent, myself);

	private static int HandResultScore(HandSign opponent, HandSign myself) =>
		(myself, opponent) switch
		{
			// 0 loose, 3 draw, 6 win
			(HandSign.Rock, HandSign.Scissor) or (HandSign.Paper, HandSign.Rock) or (HandSign.Scissor, HandSign.Paper) => 6,	// win
			(HandSign.Rock, HandSign.Rock) or (HandSign.Paper, HandSign.Paper) or (HandSign.Scissor, HandSign.Scissor) => 3,	// draw
			(HandSign.Rock, HandSign.Paper) or (HandSign.Paper, HandSign.Scissor) or (HandSign.Scissor, HandSign.Rock) => 0,	// loose
			_ => 0
		};

	private static int HandSignScore(HandSign hand) =>
		hand switch
		{
			HandSign.Rock => 1,
			HandSign.Paper => 2,
			_ => 3
		};

	private static HandSign ToHandSign(string handSign) =>
		handSign switch
		{
			"A" or "X" => HandSign.Rock,
			"B" or "Y" => HandSign.Paper,
			_ => HandSign.Scissor
		};
	
	private static Result ToExpectedResult(string expectedResult) =>
		expectedResult switch
		{
			"X" => Result.Loose,
			"Y" => Result.Draw,
			_ => Result.Win
		};
}