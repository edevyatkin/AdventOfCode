namespace AdventOfCode2024.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData(new [] {
        "AAAA",
        "BBCD",
        "BBCC",
        "EEEC"
    }, 140)]
    [InlineData(new [] {
        "OOOOO",
        "OXOXO",
        "OOOOO",
        "OXOXO",
        "OOOOO"
    }, 772)]
    [InlineData(new [] {
        "RRRRIICCFF",
        "RRRRIICCCF",
        "VVRRRCCFFF",
        "VVRCCCJFFF",
        "VVVVCJJCFE",
        "VVIVCCJJEE",
        "VVIIICJJEE",
        "MIIIIIJJEE",
        "MIIISIJEEE",
        "MMMISSJEEE"
    }, 1930)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day12().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "AAAA",
        "BBCD",
        "BBCC",
        "EEEC"
    }, 80)]
    [InlineData(new [] {
        "OOOOO",
        "OXOXO",
        "OOOOO",
        "OXOXO",
        "OOOOO"
    }, 436)]
    [InlineData(new [] {
        "EEEEE",
        "EXXXX",
        "EEEEE",
        "EXXXX",
        "EEEEE"
    }, 236)]
    [InlineData(new [] {
        "AAAAAA",
        "AAABBA",
        "AAABBA",
        "ABBAAA",
        "ABBAAA",
        "AAAAAA"
    }, 368)]
    [InlineData(new [] {
        "RRRRIICCFF",
        "RRRRIICCCF",
        "VVRRRCCFFF",
        "VVRCCCJFFF",
        "VVVVCJJCFE",
        "VVIVCCJJEE",
        "VVIIICJJEE",
        "MIIIIIJJEE",
        "MIIISIJEEE",
        "MMMISSJEEE"
    }, 1206)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day12().Solve(input).Part2);
    }
}
