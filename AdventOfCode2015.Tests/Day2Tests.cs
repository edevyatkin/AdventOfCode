using Xunit;

namespace AdventOfCode2015.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData("2x3x4", 58)]
    [InlineData("1x1x10", 43)]
    public void Part1Test(string present, int result)
    {
        Assert.Equal(result, new Day2().Solve(new[] { present }).Part1);
    }
    
    [Theory]
    [InlineData("2x3x4", 34)]
    [InlineData("1x1x10", 14)]
    public void Part2Test(string present, int result)
    {
        Assert.Equal(result, new Day2().Solve(new[] { present }).Part2);
    }
}