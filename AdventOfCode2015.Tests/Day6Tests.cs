using Xunit;

namespace AdventOfCode2015.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData("turn on 0,0 through 999,999", 1_000_000)]
    [InlineData("turn off 0,0 through 999,999", 0)]
    [InlineData("turn on 1,0 through 1,999", 1_000)]
    [InlineData("toggle 1,0 through 1,999", 1_000)]
    public void Part1Test(string str, int result)
    {
        Assert.Equal(result, new Day6().Solve(new[] { str }).Part1);
    }
    
    [Theory]
    [InlineData("turn on 0,0 through 999,999", 1_000_000)]
    [InlineData("turn off 0,0 through 999,999", 0)]
    [InlineData("turn on 1,0 through 1,999", 1_000)]
    [InlineData("toggle 1,0 through 1,999", 2_000)]
    public void Part2Test(string str, int result)
    {
        Assert.Equal(result, new Day6().Solve(new[] { str }).Part2);
    }
}
