namespace AdventOfCode2024.Tests;

public class Day8Tests
{
    [Theory]
    [InlineData(new [] {
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............"
    }, 14)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day8().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new [] {
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............"
    }, 34)]
    [InlineData(new [] {
        "T.........",
        "...T......",
        ".T........",
        "..........",
        "..........",
        "..........",
        "..........",
        "..........",
        "..........",
        ".........."
    }, 9)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day8().Solve(input).Part2);
    }
}
