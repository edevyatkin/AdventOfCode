namespace AdventOfCode2017.Tests;

public class Day15Tests
{
    [Theory]
    [InlineData(65,8921, 588)]
    public void Part1Test(int gen1, int gen2, int result)
    {
        Assert.Equal(result, Day15.CountMatches(gen1, gen2, 40_000_000, false));
    }
    
    [Theory]
    [InlineData(65,8921, 309)]
    public void Part2Test(int gen1, int gen2, int result)
    {
        Assert.Equal(result, Day15.CountMatches(gen1, gen2, 5_000_000, true));
    }
}
