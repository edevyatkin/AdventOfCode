using Xunit;

namespace AdventOfCode2022.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData(new[]
    {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    }, 2)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day4().Solve(data).Part1);
    }

    [Theory]
    [InlineData(new[]
    {
        "2-4,6-8",
        "2-3,4-5",
        "5-7,7-9",
        "2-8,3-7",
        "6-6,4-6",
        "2-6,4-8"
    }, 4)]
    public void Part2Test(string[] data, int result)
    {
        Assert.Equal(result, new Day4().Solve(data).Part2);
    }
}
