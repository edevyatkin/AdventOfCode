namespace AdventOfCode2023.Tests;

public class Day7Tests
{
    [Theory]
    [InlineData(new[] {
        "32T3K 765",
        "T55J5 684",
        "KK677 28",
        "KTJJT 220",
        "QQQJA 483"
    }, 6440)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, Day7.SolvePart(input, false));
    }

    [Theory]
    [InlineData(new[] {
        "32T3K 765",
        "T55J5 684",
        "KK677 28",
        "KTJJT 220",
        "QQQJA 483"
    }, 5905)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, Day7.SolvePart(input, true));
    }
}
