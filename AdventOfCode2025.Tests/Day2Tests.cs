namespace AdventOfCode2025.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new[] {
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224," +
        "1698522-1698528,446443-446449,38593856-38593862,565653-565659," +
        "824824821-824824827,2121212118-2121212124"
    }, 1227775554)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day2().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224," +
        "1698522-1698528,446443-446449,38593856-38593862,565653-565659," +
        "824824821-824824827,2121212118-2121212124"
    }, 4174379265)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day2().Solve(input).Part2);
    }
}
