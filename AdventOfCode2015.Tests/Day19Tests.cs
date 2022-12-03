using Xunit;

namespace AdventOfCode2015.Tests;

public class Day19Tests
{
    [Theory]
    [InlineData(new[] { "H => HO","H => OH","O => HH","","HOH" }, 4)]
    [InlineData(new[] { "H => HO","H => OH","O => HH","","HOHOHO" }, 7)]
    [InlineData(new[] { "H => OO","","H2O" }, 1)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day19().Solve(data).Part1);
    }

    [Theory]
    [InlineData(new [] {"e => H", "e => O", "H => HO", "H => OH", "O => HH","","HOH"}, 3)]
    [InlineData(new [] {"e => H", "e => O", "H => HO", "H => OH", "O => HH","","HOHOHO"}, 6)]
    public void Part2Test(string[] data, int result)
    {
        Assert.Equal(result, new Day19().Solve(data).Part2);
    }
}
