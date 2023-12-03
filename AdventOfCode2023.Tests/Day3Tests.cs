namespace AdventOfCode2023.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(new[] {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
    }, 4361)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
    }, 467835)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day3().Solve(input).Part2);
    }
}
