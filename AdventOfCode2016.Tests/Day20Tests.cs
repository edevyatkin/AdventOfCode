using Xunit;

namespace AdventOfCode2016.Tests;

public class Day20Tests
{
    [Theory]
    [InlineData(new[]
    {
        "5-8",
        "0-2",
        "4-7"
    }, 3)]
    public void Part1Test(string[] rules, long minAllowedIp)
    {
        Assert.Equal(minAllowedIp, new Day20().Solve(rules).Part1);
    }
}
