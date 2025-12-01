using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(@"30373
25512
65332
33549
35390", 21)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day8().Solve(data.Split(Environment.NewLine)).Part1);
    }
}
