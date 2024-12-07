namespace AdventOfCode2024.Tests;

public class Day7Tests
{
    [Theory]
    [InlineData(new [] {
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20"
    }, 3749)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day7().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20"
    }, 11387)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day7().Solve(input).Part2);
    }
}
