namespace AdventOfCode2017.Tests;

public class Day19Tests
{
    [Theory]
    [InlineData(new [] { 
        "     |          ",
        "     |  +--+    ",
        "     A  |  C    ",
        " F---|----E|--+ ",
        "     |  |  |  D ",
        "     +B-+  +--+ "
    }, "ABCDEF")]
    public void Part1Test(string[] input, string result)
    {
        Assert.Equal(result, new Day19().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] { 
        "     |          ",
        "     |  +--+    ",
        "     A  |  C    ",
        " F---|----E|--+ ",
        "     |  |  |  D ",
        "     +B-+  +--+ "
    }, 38)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day19().Solve(input).Part2);
    }
}
