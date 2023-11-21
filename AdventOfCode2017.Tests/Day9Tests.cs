namespace AdventOfCode2017.Tests;

public class Day9Tests 
{
    [Theory]
    [InlineData("{}", 1)]
    [InlineData("{{{}}}", 6)]
    [InlineData("{{},{}}", 5)]
    [InlineData("{{{},{},{{}}}}", 16)]
    [InlineData("{<a>,<a>,<a>,<a>}", 1)]
    [InlineData("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
    [InlineData("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
    public void Part1Test(string input, int result) 
    {
        Assert.Equal(result, new Day9().Solve(new[] { input }).Part1);
    }

    [Theory]
    [InlineData("<>", 0)]
    [InlineData("<random characters>", 17)]
    [InlineData("<<<<>", 3)]
    [InlineData("<{!>}>", 2)]
    [InlineData("<!!>", 0)]
    [InlineData("<!!!>>", 0)]
    [InlineData("<{o\"i!a,<{i<a>", 10)]
    public void Part2Test(string input, int result) 
    {
        Assert.Equal(result, new Day9().Solve(new[] { input }).Part2);
    }
}
