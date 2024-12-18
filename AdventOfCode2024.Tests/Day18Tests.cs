namespace AdventOfCode2024.Tests;

public class Day18Tests
{
    [Theory]
    [InlineData(new [] {
        "5,4",
        "4,2",
        "4,5",
        "3,0",
        "2,1",
        "6,3",
        "2,4",
        "1,5",
        "0,6",
        "3,3",
        "2,6",
        "5,1",
        "1,2",
        "5,5",
        "2,5",
        "6,5",
        "1,4",
        "0,4",
        "6,4",
        "1,1",
        "6,1",
        "1,0",
        "0,5",
        "1,6",
        "2,0"
    }, 22)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day18().SolvePart(input,7,7,12, false));
    }
    
    [Theory]
    [InlineData(new [] {
        "5,4",
        "4,2",
        "4,5",
        "3,0",
        "2,1",
        "6,3",
        "2,4",
        "1,5",
        "0,6",
        "3,3",
        "2,6",
        "5,1",
        "1,2",
        "5,5",
        "2,5",
        "6,5",
        "1,4",
        "0,4",
        "6,4",
        "1,1",
        "6,1",
        "1,0",
        "0,5",
        "1,6",
        "2,0"
    }, "6,1")]
    public void Part2Test(string[] input, string result)
    {
        Assert.Equal(result, new Day18().SolvePart(input,7,7,12, true));

    }
}
