using Xunit;

namespace AdventOfCode2015.Tests;

public class Day1Tests
{
    [Theory]
    [InlineData("(())", 0)]
    [InlineData("()()", 0)]
    [InlineData("(((", 3)]
    [InlineData("(()(()(", 3)]
    [InlineData("))(((((", 3)]
    [InlineData("())", -1)]
    [InlineData("))(", -1)]
    [InlineData(")))", -3)]
    [InlineData(")())())", -3)]
    public void Part1Test(string instr, int result)
    {
        Assert.Equal(result, new Day1().Solve(new[] { instr }).Part1);
    }
    
    [Theory]
    [InlineData(")", 1)]
    [InlineData("()())", 5)]
    public void Part2Test(string instr, int result)
    {
        Assert.Equal(result, new Day1().Solve(new[] { instr }).Part2);
    }
}