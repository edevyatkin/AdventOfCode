using Xunit;

namespace AdventOfCode2015.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData("abcdef", 609043)]
    [InlineData("pqrstuv", 1048970)]
    public void Part1Test(string str, int result)
    {
        Assert.Equal(result, new Day4().Solve(new[] { str }).Part1);
    }
}