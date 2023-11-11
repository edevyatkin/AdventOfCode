namespace AdventOfCode2017.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData(new []
    {
        "0",
        "3",
        "0",
        "1",
        "-3"
    },5)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, Day5.SolvePart(input, false));
    }
    
    [Theory]
    [InlineData(new []
    {
        "0",
        "3",
        "0",
        "1",
        "-3"
    },10)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, Day5.SolvePart(input, true));
    }
}
