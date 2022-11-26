using Xunit;

namespace AdventOfCode2015.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData("\"\"", 2)]
    [InlineData("\"abc\"", 2)]
    [InlineData("\"aaa\\\"aaa\"", 3)]
    [InlineData("\"\\x27\"", 5)]
    public void Part1Test(string str, int result)
    {
        Assert.Equal(result, new Day8().Solve(new[] { str }).Part1);
    }
    
    [Theory]
    [InlineData("\"\"", 4)]
    [InlineData("\"abc\"", 4)]
    [InlineData("\"aaa\\\"aaa\"", 6)]
    [InlineData("\"\\x27\"", 5)]
    public void Part2Test(string str, int result)
    {
        Assert.Equal(result, new Day8().Solve(new[] { str }).Part2);
    }
}
