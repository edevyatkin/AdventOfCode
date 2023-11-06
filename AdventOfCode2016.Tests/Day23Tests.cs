using Xunit;

namespace AdventOfCode2016.Tests;

public class Day23Tests
{
    [Theory]
    [InlineData(new[]
    {
        "cpy 2 a",
        "tgl a",
        "tgl a",
        "tgl a",
        "cpy 1 a",
        "dec a",
        "dec a"
    }, 3)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day23().SolvePart(input, false));
    }
}
