using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi", 31)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day12().Solve(data.Split(Environment.NewLine)).Part1);
    }
    
    [Theory]
    [InlineData(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi", 29)]
    public void Part2Test(string data, int result)
    {
        Assert.Equal(result, new Day12().Solve(data.Split(Environment.NewLine)).Part2);
    }
}
