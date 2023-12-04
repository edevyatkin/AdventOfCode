namespace AdventOfCode2017.Tests;

public class Day17Tests
{
    [Theory]
    [InlineData("3", 638)]
    public void Part1Test(string input, int result)
    {
        Assert.Equal(result, new Day17().Solve(new [] { input }).Part1);
    }
}
