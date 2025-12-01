using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day24Tests
{
    [Theory]
    [InlineData(@"#.######
#>>.<^<#
#.<..<<#
#>v.><>#
#<^v^^>#
######.#", 18)]
    public void Part1Test(string data, int result)
    { 
        Assert.Equal(result, new Day24().Solve(data.Split(Environment.NewLine)).Part1);
    }
    
    [Theory]
    [InlineData(@"#.######
#>>.<^<#
#.<..<<#
#>v.><>#
#<^v^^>#
######.#", 54)]
    public void Part2Test(string data, int result)
    { 
        Assert.Equal(result, new Day24().Solve(data.Split(Environment.NewLine)).Part2);
    }
}
