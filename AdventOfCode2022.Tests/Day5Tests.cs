using Xunit;

namespace AdventOfCode2022.Tests;

public class Day5Tests
{
    [Theory]
    [InlineData(new[]
    {
        "    [D]    ",
        "[N] [C]    ",    
        "[Z] [M] [P]",
        " 1   2   3 ",
        "",
        "move 1 from 2 to 1",
        "move 3 from 1 to 3",
        "move 2 from 2 to 1",
        "move 1 from 1 to 2"
    }, "CMZ")]
    public void Part1Test(string[] data, string result)
    {
        Assert.Equal(result, new Day5().Solve(data).Part1);
    }

    [Theory]
    [InlineData(new[]
    {
        "    [D]    ",
        "[N] [C]    ",    
        "[Z] [M] [P]",
        " 1   2   3 ",
        "",
        "move 1 from 2 to 1",
        "move 3 from 1 to 3",
        "move 2 from 2 to 1",
        "move 1 from 1 to 2"
    }, "MCD")]
    public void Part2Test(string[] data, string result)
    {
        Assert.Equal(result, new Day5().Solve(data).Part2);
    }
}
