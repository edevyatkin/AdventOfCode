using Xunit;

namespace AdventOfCode2016.Tests;

public class Day14Tests
{
    [Theory]
    [InlineData(new[] { "abc" }, 22728)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, Day14.SolvePart(data[0],Day14.Md5));
    }
    
    [Theory]
    [InlineData(new[] { "abc" }, 22551)]
    public void Part2Test(string[] data, int result)
    {
        Assert.Equal(result, Day14.SolvePart(data[0],Day14.Md5Extra2016));
    }
}
