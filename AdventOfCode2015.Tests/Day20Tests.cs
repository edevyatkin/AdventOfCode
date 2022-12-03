using Xunit;

namespace AdventOfCode2015.Tests;

public class Day20Tests
{
    [Theory]
    [InlineData(new[] { "70" }, 4)]
    [InlineData(new[] { "100" }, 6)]
    [InlineData(new[] { "130" }, 8)]
    [InlineData(new[] { "150" }, 8)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day20().Solve(data).Part1);
    }
}
