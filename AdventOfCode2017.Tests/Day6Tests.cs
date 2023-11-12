namespace AdventOfCode2017.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData("0\t2\t7\t0", 5)]
    public void Part1Test(string input, int result)
    {
        Assert.Equal(result, new Day6().Solve(new[] { input }).Part1);
    }
    
    [Theory]
    [InlineData("0\t2\t7\t0", 4)]
    public void Part2Test(string input, int result)
    {
        Assert.Equal(result, new Day6().Solve(new[] { input }).Part2);
    }
}
