using Xunit;

namespace AdventOfCode2016.Tests;

public class Day12Tests
{
    [Theory]
    [InlineData(new[]
    {
        "cpy 41 a",
        "inc a",
        "inc a",
        "dec a",
        "jnz a 2",
        "dec a"
    }, 42)]
    public void Part1Test(string[] data, int result)
    {
        Assert.Equal(result, new Day12().Solve(data).Part1);
    }
}
