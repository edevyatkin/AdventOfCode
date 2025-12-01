using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day18Tests
{
    [Theory]
    [InlineData(@"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5", 64)]
    public void Part1Test(string data, int result)
    { 
        Assert.Equal(result, new Day18().Solve(data.Split(Environment.NewLine)).Part1);
    } 
    
    [Theory]
    [InlineData(@"2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5", 58)]
    public void Part2Test(string data, int result)
    { 
        Assert.Equal(result, new Day18().Solve(data.Split(Environment.NewLine)).Part2);
    } 
}
