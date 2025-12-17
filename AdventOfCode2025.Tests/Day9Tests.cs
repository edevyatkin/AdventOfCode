namespace AdventOfCode2025.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(new[] {
        "7,1",
        "11,1",
        "11,7",
        "9,7",
        "9,5",
        "2,5",
        "2,3",
        "7,3"
    }, 50)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day9().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "7,1",
        "11,1",
        "11,7",
        "9,7",
        "9,5",
        "2,5",
        "2,3",
        "7,3"
    }, 24)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day9().Solve(input).Part2);
    }
}
