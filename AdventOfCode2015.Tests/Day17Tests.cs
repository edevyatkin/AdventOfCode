using Xunit;

namespace AdventOfCode2015.Tests;

public class Day17Tests
{
    [Theory]
    [InlineData(new[] { "20","15","10","5","5" }, 25, 4)]
    public void Part1Test(string[] containers, int litres, int result)
    {
        Assert.Equal(result, new Day17().CountCombinations(containers, litres, false));
    }
    
    [Theory]
    [InlineData(new[] { "20","15","10","5","5" }, 25, 3)]
    public void Part2Test(string[] containers, int litres, int result)
    {
        Assert.Equal(result, new Day17().CountCombinations(containers, litres, true));
    }
}
