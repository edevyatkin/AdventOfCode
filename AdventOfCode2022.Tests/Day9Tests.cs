using Xunit;

namespace AdventOfCode2022.Tests;

public class Day9Tests
{
    [Theory]
    [InlineData(@"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2", 2, 13)]
    public void Part1Test(string data, int knots, int result)
    {
        Assert.Equal(result, new Day9().TailVisited(data.Split('\n'), knots));
    }
    
    
    [Theory]
    [InlineData(@"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20", 10, 36)]
    public void Part2Test(string data, int knots, int result)
    {
        Assert.Equal(result, new Day9().TailVisited(data.Split('\n'), knots));
    }
}
