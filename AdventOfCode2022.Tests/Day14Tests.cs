using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day14Tests
{
    [Theory]
    [InlineData(@"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9", 24)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day14().Solve(data.Split(Environment.NewLine)).Part1);
    }   
    
    [Theory]
    [InlineData(@"498,4 -> 498,6 -> 496,6
503,4 -> 502,4 -> 502,9 -> 494,9", 93)]
    public void Part2Test(string data, int result)
    {
        Assert.Equal(result, new Day14().Solve(data.Split(Environment.NewLine)).Part2);
    }  
}
