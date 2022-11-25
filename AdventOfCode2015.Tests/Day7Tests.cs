using Xunit;

namespace AdventOfCode2015.Tests;

public class Day7Tests
{
    [Theory]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> a", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i"}, 72)]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> d", "x OR y -> a", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i"}, 507)]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> a", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i"}, 492)]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> a", "NOT x -> h", "NOT y -> i"}, 114)]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> a", "NOT y -> i"}, 65412)]
    [InlineData(new[] {"123 -> x", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> a"}, 65079)]
    [InlineData(new[] {"123 -> a", "456 -> y", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i"}, 123)]
    [InlineData(new[] {"123 -> x", "456 -> a", "x AND y -> d", "x OR y -> e", "x LSHIFT 2 -> f", "y RSHIFT 2 -> g", "NOT x -> h", "NOT y -> i"}, 456)]
    public void Part1Test(string[] wires, int result)
    {
        Assert.Equal(result, new Day7().Solve(wires).Part1);
    }
}
