using Xunit;

namespace AdventOfCode2022.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new[] { "A Y","B X","C Z" }, 15)]
    public void Part1Test(string[] containers, int result)
    {
        Assert.Equal(result, new Day2().Solve(containers).Part1);
    }
    
    [Theory]
    [InlineData(new[] { "A Y","B X","C Z" }, 12)]
    public void Part2Test(string[] containers, int result)
    {
        Assert.Equal(result, new Day2().Solve(containers).Part2);
    }
}
