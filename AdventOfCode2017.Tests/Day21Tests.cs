namespace AdventOfCode2017.Tests;

public class Day21Tests
{
    [Theory]
    [InlineData(new [] {
        "../.# => ##./#../...",
        ".#./..#/### => #..#/..../..../#..#"
    }, 2, 12)]
    public void Part1Test(string[] input, int iterations, long result)
    {
        var rules = RuleParser.Parse(input);
        Assert.Equal(result, Day21.Solve(rules, iterations));
    }
}
