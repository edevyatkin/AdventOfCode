using Xunit;

namespace AdventOfCode2016.Tests;

public class Day15Tests
{
    [Theory]
    [InlineData(new[] { 
        "Disc #1 has 5 positions; at time=0, it is at position 4.",
        "Disc #2 has 2 positions; at time=0, it is at position 1." 
    }, 5)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day15().Solve(data).Part1);
    }
}
