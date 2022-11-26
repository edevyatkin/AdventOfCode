using Xunit;

namespace AdventOfCode2015.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData("1", 2)]
    [InlineData("11", 2)]
    [InlineData("1211", 6)]
    [InlineData("111221", 6)]
    public void CommonTest(string str, int result)
    {
        Assert.Equal(result, new Day10().BuildString(str, 1));
    }
}
