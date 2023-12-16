namespace AdventOfCode2023.Tests;

public class Day16Tests
{
    [Theory]
    [InlineData(new[] { 
        ".|...\\....",
        "|.-.\\.....",
        ".....|-...",
        "........|.",
        "..........",
        ".........\\",
        "..../.\\\\..",
        ".-.-/..|..",
        ".|....-|.\\",
        "..//.|...."
    }, 46)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day16().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new[] {
        ".|...\\....",
        "|.-.\\.....",
        ".....|-...",
        "........|.",
        "..........",
        ".........\\",
        "..../.\\\\..",
        ".-.-/..|..",
        ".|....-|.\\",
        "..//.|...."
    }, 51)]
    public void Part2Test(string[] input, int result)
    {
        Assert.Equal(result, new Day16().Solve(input).Part2);
    }
}
