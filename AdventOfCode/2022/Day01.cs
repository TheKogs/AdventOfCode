namespace AdventOfCode._2022;

public static class Day01
{
    public static void Execute(string input)
    {
        var sortedCaloriesList = input
            .Trim()
            .Split($"{Environment.NewLine}{Environment.NewLine}") 
            .Select(elvesCalories =>
                elvesCalories
                    .Split(Environment.NewLine)
                    .Sum(int.Parse))
            .OrderByDescending(x => x)
            .ToList();
        
        Console.WriteLine("Day01");
        Console.WriteLine($"\tTop elv calories = {sortedCaloriesList.First()}");
        Console.WriteLine($"\tTop 3 elves calories = {sortedCaloriesList.Take(3).Sum()}");
    }
  
}