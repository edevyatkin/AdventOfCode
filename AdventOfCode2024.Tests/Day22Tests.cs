namespace AdventOfCode2024.Tests;

public class Day22Tests
{
    [Theory]
    [InlineData(new [] { 
        "1",
        "10",
        "100",
        "2024",
    }, 37327623)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day22().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] { 
        "1",
        "2",
        "3",
        "2024",
    }, 23)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day22().Solve(input).Part2);
    }
}
