using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day13Tests
{
    [Theory]
    [InlineData("[1,1,3,1,1]\n[1,1,5,1,1]", 1)]
    [InlineData("[[1],[2,3,4]]\n[[1],4]", 1)]
    [InlineData("[9]\n[[8,7,6]]", 0)]
    [InlineData("[[4,4],4,4]\n[[4,4],4,4,4]",1)]
    [InlineData("[7,7,7,7]\n[7,7,7]",0)]    
    [InlineData("[]\n[3]",1)]    
    [InlineData("[[[]]]\n[[]]",0)]    
    [InlineData("[1,[2,[3,[4,[5,6,7]]]],8,9]\n[1,[2,[3,[4,[5,6,0]]]],8,9]",0)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day13().Solve(data.Split('\n')).Part1);
    }
    
    [Theory]
    [InlineData(@"[1,1,3,1,1]
[1,1,5,1,1]

[[1],[2,3,4]]
[[1],4]

[9]
[[8,7,6]]

[[4,4],4,4]
[[4,4],4,4,4]

[7,7,7,7]
[7,7,7]

[]
[3]

[[[]]]
[[]]

[1,[2,[3,[4,[5,6,7]]]],8,9]
[1,[2,[3,[4,[5,6,0]]]],8,9]", 140)]
    public void Part2Test(string data, int result)
    {
        Assert.Equal(result, new Day13().Solve(data.Split(Environment.NewLine)).Part2);
    }
}
