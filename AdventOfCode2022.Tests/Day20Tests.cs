using Xunit;

namespace AdventOfCode2022.Tests;

public class Day20Tests
{
    [Theory]
    [InlineData(@"1
2
-3
3
-2
0
4", 3)]
    public void Part1Test(string data, long result)
    { 
        Assert.Equal(result, new Day20().Solve(data.Split('\n')).Part1);
    }
    
    [Theory]
    [InlineData(@"1
2
-3
3
-2
0
4", 1623178306)]
    public void Part2Test(string data, long result)
    { 
        Assert.Equal(result, new Day20().Solve(data.Split('\n')).Part2);
    }
}
