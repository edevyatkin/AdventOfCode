using Xunit;

namespace AdventOfCode2015.Tests;

public class Day18Tests
{
    [Theory]
    [InlineData(new[] { ".#.#.#","...##.","#....#","..#...","#.#..#","####.." }, 4, 4)]
    public void Part1Test(string[] data, int steps, int result)
    {
        Assert.Equal(result, new Day18().CountLights(data, steps, false));
    }
    
    [Theory]
    [InlineData(new[] { ".#.#.#","...##.","#....#","..#...","#.#..#","####.." }, 5, 17)]
    public void Part2Test(string[] data, int steps, int result)
    {
        Assert.Equal(result, new Day18().CountLights(data, steps, true));
    }
}
