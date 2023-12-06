namespace AdventOfCode2023.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData(new[] {
        "Time:      7  15   30",
        "Distance:  9  40  200"
    }, 288L)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "Time:      7  15   30",
        "Distance:  9  40  200"
    }, 71503L)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part2);
    }
}
