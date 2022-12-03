using Xunit;

namespace AdventOfCode2022.Tests;

public class Day3Tests
{
    [Theory]
    [InlineData(new[]
    {
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
    }, 157)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day3().Solve(data).Part1);
    }

    [Theory]
    [InlineData(new[]
    {
        "vJrwpWtwJgWrhcsFMMfFFhFp",
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
        "PmmdzqPrVvPwwTWBwg",
        "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
        "ttgJtRGJQctTZtZT",
        "CrZsJsPPZsGzwwsLwLmpwMDw"
    }, 70)]
    public void Part2Test(string[] data, int result)
    {
        Assert.Equal(result, new Day3().Solve(data).Part2);
    }
}
