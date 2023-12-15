namespace AdventOfCode2023.Tests;

public class Day15Tests
{
    [Theory]
    [InlineData(new[] {
        "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
    }, 1320)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day15().Solve(input).Part1);
    }
    
    [Theory]
    [InlineData(new[] {
        "rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7"
    }, 145)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day15().Solve(input).Part2);
    }
}
