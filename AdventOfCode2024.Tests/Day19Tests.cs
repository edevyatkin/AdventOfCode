namespace AdventOfCode2024.Tests;

public class Day19Tests
{
    [Theory]
    [InlineData(new [] { 
        "r, wr, b, g, bwu, rb, gb, br",
        "",
        "brwrr",
        "bggr",
        "gbbr",
        "rrbgbr",
        "ubwu",
        "bwurrg",
        "brgr",
        "bbrgwb"
    }, 6)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day19().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] { 
        "r, wr, b, g, bwu, rb, gb, br",
        "",
        "brwrr",
        "bggr",
        "gbbr",
        "rrbgbr",
        "ubwu",
        "bwurrg",
        "brgr",
        "bbrgwb"
    }, 16)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day19().Solve(input).Part2);
    }
}
