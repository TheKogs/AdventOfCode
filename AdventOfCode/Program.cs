﻿// See https://aka.ms/new-console-template for more information

using AdventOfCode._2022;

Console.WriteLine("Hello, AdventOfCode");

Day01.Execute(File.ReadAllText("2022/Day01_Input.txt"));
Day02.Execute(File.ReadAllText("2022/Day02_Input.txt"));
var x = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw";
// Day03.Execute(x);
Day03.Execute(File.ReadAllText("2022/Day03_Input.txt"));