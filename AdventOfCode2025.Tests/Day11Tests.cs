namespace AdventOfCode2025.Tests;

public class Day11Tests
{
    [Theory]
    [InlineData(new[] {
        "aaa: you hhh",
        "you: bbb ccc",
        "bbb: ddd eee",
        "ccc: ddd eee fff",
        "ddd: ggg",
        "eee: out",
        "fff: out",
        "ggg: out",
        "hhh: ccc fff iii",
        "iii: out"
    }, 5)]
    public void Part1Test(string[] input, long result)
    {
        Assert.Equal(result, new Day11().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "svr: aaa bbb",
        "aaa: fft",
        "fft: ccc",
        "bbb: tty",
        "tty: ccc",
        "ccc: ddd eee",
        "ddd: hub",
        "hub: fff",
        "eee: dac",
        "dac: fff",
        "fff: ggg hhh",
        "ggg: out",
        "hhh: out"
    }, 2)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day11().Solve(input).Part2);
    }
}
