namespace AdventOfCode2023.Tests;

public class Day14Tests
{
    [Theory]
    [InlineData(new[] { 
        "O....#....",
        "O.OO#....#",
        ".....##...",
        "OO.#O....O",
        ".O.....O#.",
        "O.#..O.#.#",
        "..O..#O..O",
        ".......O..",
        "#....###..",
        "#OO..#...."
    }, 136)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day14().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new[] { 
        "O....#....",
        "O.OO#....#",
        ".....##...",
        "OO.#O....O",
        ".O.....O#.",
        "O.#..O.#.#",
        "..O..#O..O",
        ".......O..",
        "#....###..",
        "#OO..#...."
    }, 64)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day14().Solve(input).Part2);
    }
    
    [Theory]
    [InlineData(new[] { 
        "O....#....",
        "O.OO#....#",
        ".....##...",
        "OO.#O....O",
        ".O.....O#.",
        "O.#..O.#.#",
        "..O..#O..O",
        ".......O..",
        "#....###..",
        "#OO..#...."
    }, new[] { 
        "OOOO.#.O..",
        "OO..#....#",
        "OO..O##..O",
        "O..#.OO...",
        "........#.",
        "..#....#.#",
        "..O..#.O.O",
        "..O.......",
        "#....###..",
        "#....#...."
    })]
    public void TiltNorthTest(string[] input1, string[] input2)
    {
        var dish1 = new Dish(input1.Select(r => r.ToCharArray()).ToArray());
        var dish2 = new Dish(input2.Select(r => r.ToCharArray()).ToArray());
        Assert.True(dish1.Tilt(Direction.North).Equals(dish2));
    }
}
