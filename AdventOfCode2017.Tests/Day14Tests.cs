namespace AdventOfCode2017.Tests;

public class Day14Tests
{
    [Theory]
    [InlineData("flqrgnkx", 8108)]
    public void Part1Test(string input, int result)
    {
        Assert.Equal(result, new Day14().Solve(new [] { input }).Part1);
    }
    
    [Theory]
    [InlineData("flqrgnkx", 1242)]
    public void Part2Test(string input, int result)
    {
        Assert.Equal(result, new Day14().Solve(new [] { input }).Part2);
    }
}
