using Xunit;

namespace AdventOfCode2015.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData("ugknbfddgicrmopn", 1)]
    [InlineData("aaa", 1)]
    [InlineData("jchzalrnumimnmhp", 0)]
    [InlineData("haegwjzuvuyypxyu", 0)]
    [InlineData("dvszwmarrgswjxmb", 0)]
    public void Part1Test(string str, int result)
    {
        Assert.Equal(result, new Day5().Solve(new[] { str }).Part1);
    }
    
    [Theory]
    [InlineData("qjhvhtzxzqqjkmpb", 1)]
    [InlineData("xxyxx", 1)]
    [InlineData("uurcxstgmygtbstg", 0)]
    [InlineData("ieodomkazucvgmuy", 0)]
    public void Part2Test(string str, int result)
    {
        Assert.Equal(result, new Day5().Solve(new[] { str }).Part2);
    }
}