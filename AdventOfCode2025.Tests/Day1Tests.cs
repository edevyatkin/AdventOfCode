namespace AdventOfCode2025.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new [] {
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3"
    }, 11)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3"
    }, 31)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part2);
    }
}
