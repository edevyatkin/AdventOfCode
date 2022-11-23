using Xunit;

namespace AdventOfCode2015.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(">", 2)]
    [InlineData("^>v<", 4)]
    [InlineData("^v^v^v^v^v", 2)]
    public void Part1Test(string moves, int result)
    {
        Assert.Equal(result, new Day3().Solve(new[] { moves }).Part1);
    }
    
    [Theory]
    [InlineData("^v", 3)]
    [InlineData("^>v<", 3)]
    [InlineData("^v^v^v^v^v", 11)]
    public void Part2Test(string moves, int result)
    {
        Assert.Equal(result, new Day3().Solve(new[] { moves }).Part2);
    }
}