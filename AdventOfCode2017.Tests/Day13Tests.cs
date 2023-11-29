namespace AdventOfCode2017.Tests;

public class Day13Tests
{
    [Theory]
    [InlineData(new[] {
        "0: 3",
        "1: 2",
        "4: 4",
        "6: 4"
    }, 24)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day13().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new[] {
        "0: 3",
        "1: 2",
        "4: 4",
        "6: 4"
    }, 10)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day13().Solve(input).Part2);
    }
}
