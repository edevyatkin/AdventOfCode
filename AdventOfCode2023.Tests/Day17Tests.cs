namespace AdventOfCode2023.Tests;

public class Day17Tests
{
    [Theory]
    [InlineData(new[] { 
        "2413432311323",
        "3215453535623",
        "3255245654254",
        "3446585845452",
        "4546657867536",
        "1438598798454",
        "4457876987766",
        "3637877979653",
        "4654967986887",
        "4564679986453",
        "1224686865563",
        "2546548887735",
        "4322674655533"
    }, 102)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day17().Solve(input).Part1);
    }
    
    
    [Theory]
    [InlineData(new[] { 
        "2413432311323",
        "3215453535623",
        "3255245654254",
        "3446585845452",
        "4546657867536",
        "1438598798454",
        "4457876987766",
        "3637877979653",
        "4654967986887",
        "4564679986453",
        "1224686865563",
        "2546548887735",
        "4322674655533"
    }, 94)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day17().Solve(input).Part2);
    }
}
