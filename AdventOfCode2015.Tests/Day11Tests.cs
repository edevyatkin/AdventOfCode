using Xunit;

namespace AdventOfCode2015.Tests;

public class Day11Tests
{
    [Theory]
    [InlineData("abcdefgh","abcdffaa")]
    [InlineData("ghijklmn","ghjaabcc")]
    public void CommonTest(string str, string result)
    {
        Assert.Equal(result, new Day11().Solve(new[] { str }).Part1);
    }
}
