using Xunit;

namespace AdventOfCode2016.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData(new[] { 
        "eedadn",
        "drvtee",
        "eandsr",
        "raavrd",
        "atevrs",
        "tsrnev",
        "sdttsa",
        "rasrtv",
        "nssdts",
        "ntnada",
        "svetve",
        "tesnvt",
        "vntsnd",
        "vrdear",
        "dvrsen",
        "enarar" }, "easter")]
    public void Part1Test(string[] messages, string result)
    {
        Assert.Equal(result, new Day6().Solve(messages).Part1);
    }
    
    [Theory]
    [InlineData(new[] { 
        "eedadn",
        "drvtee",
        "eandsr",
        "raavrd",
        "atevrs",
        "tsrnev",
        "sdttsa",
        "rasrtv",
        "nssdts",
        "ntnada",
        "svetve",
        "tesnvt",
        "vntsnd",
        "vrdear",
        "dvrsen",
        "enarar" }, "advent")]
    public void Part2Test(string[] messages, string result)
    {
        Assert.Equal(result, new Day6().Solve(messages).Part2);
    }
}
