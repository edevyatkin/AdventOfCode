namespace AdventOfCode2019.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData(new [] { 
        "<x=-1, y=0, z=2>",
        "<x=2, y=-10, z=-7>",
        "<x=4, y=-8, z=8>",
        "<x=3, y=5, z=-1>" 
    }, 10, 179)]
    [InlineData(new [] { 
        "<x=-8, y=-10, z=0>",
        "<x=5, y=5, z=10>",
        "<x=2, y=-7, z=3>",
        "<x=9, y=-8, z=-3>" 
    }, 100, 1940)]
    public void Part1Test(string[] input, int steps, int result)
    {
        Assert.Equal(result, new Day12().SolvePart1(input, steps));
    }
}
