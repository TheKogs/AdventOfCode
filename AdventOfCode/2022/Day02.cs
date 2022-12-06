namespace AdventOfCode._2022;

public static class Extensions {
	public static void Deconstruct<T>(this IList<T> list, out T first, out T second) {
		first = list.Count > 0 ? list[0] : default(T); // or throw
		second = list.Count > 1 ? list[1] : default(T); // or throw
	}
}
public static class Day02
{
	private enum HandSign
	{
		Rock,
		Paper,
		Scissor
	};

	private enum Result
	{
		Lose,
		Draw,
		Win
	};

	public static void Execute(string input)
	{
		var allRounds = input.Trim().Split(Environment.NewLine)
			.Select(round => round.Trim().Split(" "))
			.ToList();
		
		var totalScorePart1 = allRounds
			.Select(round => (Opponent: ToHandSign(round[0]), Me: ToHandSign(round[1])))
			.Select(round => CalcTotalScore(round.Opponent, round.Me))
			.Sum();
		
		var totalScorePart2 = allRounds
			.Select(round => (Hand: ToHandSign(round[0]), Result: ToExpectedResult(round[1])))
			.Select(round => CalcTotalScore(round.Hand, CalcMove(round.Hand, round.Result)))
			.Sum();

		Console.WriteLine("Day02");
		Console.WriteLine($"\tPoints Part 1: {totalScorePart1}");
		Console.WriteLine($"\tPoints Part 2: {totalScorePart2}");
	}

	private static HandSign CalcMove(HandSign handSign, Result expectedResult) =>
		(handSign, expectedResult) switch
		{
			(HandSign.Rock, Result.Draw) or (HandSign.Paper, Result.Lose) or (HandSign.Scissor, Result.Win) => HandSign.Rock,
			(HandSign.Rock, Result.Lose) or (HandSign.Paper, Result.Win) or (HandSign.Scissor, Result.Draw) => HandSign.Scissor,
			(HandSign.Rock, Result.Win) or (HandSign.Paper, Result.Draw) or (HandSign.Scissor, Result.Lose) => HandSign.Paper,
			_ => throw new Exception("Invalid hand sign / expected result")		// default should never be reached
		};

	private static int CalcTotalScore(HandSign opponent, HandSign myself) => HandSignScore(myself) + HandResultScore(opponent, myself);

	private static int HandResultScore(HandSign opponent, HandSign myself) =>
		(myself, opponent) switch
		{
			(HandSign.Rock, HandSign.Scissor) or (HandSign.Paper, HandSign.Rock) or (HandSign.Scissor, HandSign.Paper) => 6,	// win
			(HandSign.Rock, HandSign.Rock) or (HandSign.Paper, HandSign.Paper) or (HandSign.Scissor, HandSign.Scissor) => 3,	// draw
			(HandSign.Rock, HandSign.Paper) or (HandSign.Paper, HandSign.Scissor) or (HandSign.Scissor, HandSign.Rock) => 0,	// lose
			_ => throw new Exception("Invalid hand signs")
		};

	private static int HandSignScore(HandSign hand) =>
		hand switch
		{
			HandSign.Rock => 1,
			HandSign.Paper => 2,
			HandSign.Scissor => 3,
			_ => throw new Exception("Invalid hand sign")
		};

	private static HandSign ToHandSign(string handSign) =>
		handSign switch
		{
			"A" or "X" => HandSign.Rock,
			"B" or "Y" => HandSign.Paper,
			"C" or "Z" => HandSign.Scissor,
			_ => throw new Exception("Invalid hand sign")
		};
	
	private static Result ToExpectedResult(string expectedResult) =>
		expectedResult switch
		{
			"X" => Result.Lose,
			"Y" => Result.Draw,
			"Z" => Result.Win,
			_ => throw new Exception("Invalid expected result")
		};
}