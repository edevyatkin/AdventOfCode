namespace AdventOfCode2019.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new [] {
        "12",
        "14",
        "1969",
        "100756"
    }, 34241)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "14",
        "1969",
        "100756"
    }, 51314)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part2);
    }
}
