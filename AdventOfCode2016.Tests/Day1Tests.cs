using Xunit;

namespace AdventOfCode2016.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new[] { "R2, L3" }, 5)]
    [InlineData(new[] { "R2, R2, R2" }, 2)]
    [InlineData(new[] { "R5, L5, R5, R3" }, 12)]
    public void Part1Test(string[] sequence, int result)
    {
        Assert.Equal(result, new Day1().Solve(sequence).Part1);
    }
    
    [Theory]
    [InlineData(new[] { "R8, R4, R4, R8" }, 4)]
    public void Part2Test(string[] sequence, int result)
    {
        Assert.Equal(result, new Day1().Solve(sequence).Part2);
    }
}
