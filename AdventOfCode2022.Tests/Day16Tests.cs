using System;
using Xunit;

namespace AdventOfCode2022.Tests;

public class Day16Tests
{
    [Theory]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II", 1651)]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valve BB
Valve BB has flow rate=13; tunnels lead to valve AA", 364)]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valve BB
Valve BB has flow rate=1; tunnels lead to valves CC, AA
Valve CC has flow rate=5; tunnels lead to valves BB", 160)]
    public void Part1Test(string data, int result)
    { 
        Assert.Equal(result, new Day16().Solve(data.Split(Environment.NewLine)).Part1);
    } 
    
    [Theory]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
Valve BB has flow rate=13; tunnels lead to valves CC, AA
Valve CC has flow rate=2; tunnels lead to valves DD, BB
Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
Valve EE has flow rate=3; tunnels lead to valves FF, DD
Valve FF has flow rate=0; tunnels lead to valves EE, GG
Valve GG has flow rate=0; tunnels lead to valves FF, HH
Valve HH has flow rate=22; tunnel leads to valve GG
Valve II has flow rate=0; tunnels lead to valves AA, JJ
Valve JJ has flow rate=21; tunnel leads to valve II", 1707)]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valve BB
Valve BB has flow rate=13; tunnels lead to valve AA", 312)]
    [InlineData(@"Valve AA has flow rate=0; tunnels lead to valve BB
Valve BB has flow rate=1; tunnels lead to valves CC, AA
Valve CC has flow rate=5; tunnels lead to valves BB", 139)]
    public void Part2Test(string data, int result)
    { 
        Assert.Equal(result, new Day16().Solve(data.Split(Environment.NewLine)).Part2);
    } 
}
