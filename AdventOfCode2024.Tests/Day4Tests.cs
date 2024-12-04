namespace AdventOfCode2024.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData(new [] {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX"
    }, 18)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day4().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "MMMSXXMASM",
        "MSAMXMSMSA",
        "AMXSXMAAMM",
        "MSAMASMSMX",
        "XMASAMXAMM",
        "XXAMMXXAMA",
        "SMSMSASXSS",
        "SAXAMASAAA",
        "MAMMMXMMMM",
        "MXMXAXMASX"
    }, 9)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day4().Solve(input).Part2);
    }
}
