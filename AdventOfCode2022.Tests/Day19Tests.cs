using Xunit;

namespace AdventOfCode2022.Tests;

public class Day19Tests
{
    [Theory]
    [InlineData(@"Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.", 33)]
    public void Part1Test(string data, int result)
    { 
        Assert.Equal(result, new Day19().Solve(data.Split('\n')).Part1);
    }
    
    [Theory]
    [InlineData(@"Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.", 3472)]
    public void Part2Test(string data, int result)
    { 
        Assert.Equal(result, new Day19().Solve(data.Split('\n')).Part2);
    } 
}
