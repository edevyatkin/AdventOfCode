namespace AdventOfCode2024.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new [] {
        "2333133121414131402",
    }, 1928)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day9().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "2333133121414131402",
    }, 2858)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day9().Solve(input).Part2);
    }
}
