namespace AdventOfCode2024.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new [] {
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
    }, 2)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day2().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
    }, 4)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day2().Solve(input).Part2);
    }
}
