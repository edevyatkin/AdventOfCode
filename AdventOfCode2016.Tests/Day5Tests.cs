using Xunit;

namespace AdventOfCode2016.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData(new[] { "abc" }, "18f47a30")]
    public void Part1Test(string[] doorId, string result)
    {
        Assert.Equal(result, new Day5().Solve(doorId).Part1);
    }

    [Theory]
    [InlineData("abc3231929", "00000155f8105dff7f56ee10fa9b9abd")]
    public void Part1Md5Test(string input, string md5)
    {
        Assert.Equal(md5, Day5.Md5(input));
    }
    
    [Theory]
    [InlineData(new[] { "abc" }, "05ace8e3")]
    public void Part2Test(string[] doorId, string result)
    {
        Assert.Equal(result, new Day5().Solve(doorId).Part2);
    }
}
