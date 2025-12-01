namespace AdventOfCode2023.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(@"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)", 2)]
    [InlineData(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)", 6)]
    public void Part1Test(string input, int result)
    {
        Assert.Equal(result, Day8.SolvePart1(input.Split(Environment.NewLine)));
    }

    [Theory]
    [InlineData(@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)", 6)]
    public void Part2Test(string input, long result)
    {
        Assert.Equal(result, Day8.SolvePart2(input.Split(Environment.NewLine)));
    }
}
