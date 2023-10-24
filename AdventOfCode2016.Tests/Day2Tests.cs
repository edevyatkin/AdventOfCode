using Xunit;

namespace AdventOfCode2016.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new[] { "ULL","RRDDD","LURDL","UUUUD" }, 1985)]
    public void Part1Test(string[] instructions, int result)
    {
        Assert.Equal(result, new Day2().Solve(instructions).Part1);
    }
    
    [Theory]
    [InlineData(new[] { "ULL","RRDDD","LURDL","UUUUD" }, "5DB3")]
    public void Part2Test(string[] sequence, string result)
    {
        Assert.Equal(result, new Day2().Solve(sequence).Part2);
    }
}
