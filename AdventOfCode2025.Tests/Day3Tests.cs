namespace AdventOfCode2025.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(new[] {
        "987654321111111",
        "811111111111119",
        "234234234234278",
        "818181911112111"
    }, 357)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "987654321111111",
        "811111111111119",
        "234234234234278",
        "818181911112111"
    }, 3121910778619)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part2);
    }
}
