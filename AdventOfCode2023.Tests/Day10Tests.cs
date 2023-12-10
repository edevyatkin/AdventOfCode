namespace AdventOfCode2023.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData(new[] {
        "-L|F7",
        "7S-7|",
        "L|7||",
        "-L-J|",
        "L|-JF"
    }, 4)]
    [InlineData(new[] {
        "..F7.",
        ".FJ|.",
        "SJ.L7",
        "|F--J",
        "LJ..."
    }, 8)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day10().Solve(input).Part1);
    }

    [Theory]
    [InlineData("...........\n.S-------7.\n.|F-----7|.\n.||.....||.\n.||.....||.\n.|L-7.F-J|.\n.|..|.|..|.\n.L--J.L--J.\n...........", 4)]
    [InlineData(".F----7F7F7F7F-7....\n.|F--7||||||||FJ....\n.||.FJ||||||||L7....\nFJL7L7LJLJ||LJ.L-7..\nL--J.L7...LJS7F-7L7.\n....F-J..F7FJ|L7L7L7\n....L7.F7||L7|.L7L7|\n.....|FJLJ|FJ|F7|.LJ\n....FJL-7.||.||||...\n....L---J.LJ.LJLJ...", 8)]
    [InlineData("FF7FSF7F7F7F7F7F---7\nL|LJ||||||||||||F--J\nFL-7LJLJ||||||LJL-77\nF--JF--7||LJLJ7F7FJ-\nL---JF-JLJ.||-FJLJJ7\n|F|F-JF---7F7-L7L|7|\n|FFJF7L7F-JF7|JL---7\n7-L-JL7||F7|L7F-7F7|\nL.L7LFJ|||||FJL7||LJ\nL7JLJL-JLJLJL--JLJ.L", 10)]
    public void Part2Test(string input, int result)
    {
        Assert.Equal(result, new Day10().Solve(input.Split('\n')).Part2);
    }
}
