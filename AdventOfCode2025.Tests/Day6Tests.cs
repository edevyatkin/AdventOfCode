namespace AdventOfCode2025.Tests;

public class Day6Tests
{
    [Theory]
    [InlineData(new[] {
        "123 328  51 64 ",
        " 45 64  387 23 ",
        "  6 98  215 314",
        "*   +   *   +  "
    }, 4277556)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "123 328  51 64 ",
        " 45 64  387 23 ",
        "  6 98  215 314",
        "*   +   *   +  "
    }, 3263827)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day6().Solve(input).Part2);
    }
}
