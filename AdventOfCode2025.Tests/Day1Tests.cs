namespace AdventOfCode2025.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData(new [] {
        "L68",
        "L30",
        "R48",
        "L5",
        "R60",
        "L55",
        "L1",
        "L99",
        "R14",
        "L82"
    }, 3)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "L68",
        "L30",
        "R48",
        "L5",
        "R60",
        "L55",
        "L1",
        "L99",
        "R14",
        "L82"
    }, 6)]
    [InlineData(new [] {
        "L49"
    }, 0)]
    [InlineData(new [] {
        "L50"
    }, 1)]
    [InlineData(new [] {
        "L51"
    }, 1)]
    [InlineData(new [] {
        "L100"
    }, 1)]
    [InlineData(new [] {
        "L149"
    }, 1)]
    [InlineData(new [] {
        "L150"
    }, 2)]
    [InlineData(new [] {
        "L151"
    }, 2)]
    [InlineData(new [] {
        "R49"
    }, 0)]
    [InlineData(new [] {
        "R50"
    }, 1)]
    [InlineData(new [] {
        "R51"
    }, 1)]
    [InlineData(new [] {
        "R100"
    }, 1)]
    [InlineData(new [] {
        "R150"
    }, 2)]
    [InlineData(new [] {
        "R1000"
    }, 10)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day1().Solve(input).Part2);
    }
}
