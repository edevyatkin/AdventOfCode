using Xunit;

namespace AdventOfCode2016.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(new[] { 
        "rect 3x2",
        "rotate column x=1 by 1",
        "rotate row y=0 by 4",
        "rotate column x=1 by 1"}, 6)]
    public void Part1Test(string[] operations, int result)
    {
        Assert.Equal(result, new Day8().Solve(operations).Part1);
    }
}
