namespace AdventOfCode2025.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData(new[] {
        "3-5",
        "10-14",
        "16-20",
        "12-18",
        "",
        "1",
        "5",
        "8",
        "11",
        "17",
        "32"
    }, 3)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day5().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "3-5",
        "10-14",
        "16-20",
        "12-18",
        "",
        "1",
        "5",
        "8",
        "11",
        "17",
        "32"
    }, 14)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day5().Solve(input).Part2);
    }
}
