namespace AdventOfCode2023.Tests;

public class Day20Tests
{
    [Theory]
    [InlineData(new [] {
        "broadcaster -> a, b, c",
        "%a -> b",
        "%b -> c",
        "%c -> inv",
        "&inv -> a"
    }, 32000000)]
    [InlineData(new [] {
        "broadcaster -> a",
        "%a -> inv, con",
        "&inv -> b",
        "%b -> con",
        "&con -> output"
    }, 11687500)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, Day20.SolvePart1(input));
    }
}
