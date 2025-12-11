namespace AdventOfCode2025.Tests;

public class Day10Tests
{
    [Theory]
    [InlineData(new[] {
        "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
        "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
        "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
    }, 7)]
    public void Part1Test(string[] input, int result)
    {
        Assert.Equal(result, new Day10().Solve(input).Part1);
    }

    [Theory]
    [InlineData(new[] {
        "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
        "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
        "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}"
    }, 33)]
    public void Part2Test(string[] input, long result)
    {
        Assert.Equal(result, new Day10().Solve(input).Part2);
    }
}
