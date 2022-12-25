using Xunit;

namespace AdventOfCode2022.Tests;

public class Day22Tests
{
    [Theory]
    [InlineData(@"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5", 6032)]
    public void Part1Test(string data, int result)
    {
        Assert.Equal(result, new Day22().CalculatePassword(data.Split('\n'), false));
    }

    [Theory]
    [InlineData(@"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5", 5031)]
    public void Part2Test(string data, int result)
    {
        Assert.Equal(result, new Day22().CalculatePassword(data.Split('\n'), true));
    }

    [Theory]
    [InlineData(4, 8, -1, -1)]
    [InlineData(8, 11, -1, 1)]
    [InlineData(7, 8, 1, -1)]
    public void Part2CornerTest(int i, int j, int di, int dj)
    {
        var data = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.".Split('\n');
        Assert.True(new Day22().SearchForInnerCorners(data).Contains((i, j, di, dj)));
    }

    [Theory]
    [InlineData(7, 3, 0, -1, 1, 3)]
    [InlineData(7, 0, 0, -1, 1, 3)]
    [InlineData(7, 0, -1, 0, 2, 0)]
    [InlineData(8, 8, -1, 0, 2, 0)]
    [InlineData(5, 11, -1, 0, 0, 2)]
    [InlineData(5, 11, 1, 0, 0, 2)]
    [InlineData(11, 10, 0, 1, 1, 3)]
    [InlineData(11, 10, 0, -1, 1, 3)]
    [InlineData(8, 15, -1, 0, 0, 2)]
    [InlineData(8, 15, 1, 0, 0, 2)]
    [InlineData(2, 8, -1, 0, 2, 0)]
    [InlineData(2, 8, 1, 0, 2, 0)]
    [InlineData(0, 8, 0, 1, 3, 1)]
    [InlineData(0, 8, 0, -1, 3, 1)]
    [InlineData(0, 8, -1, 0, 2, 0)]
    [InlineData(0, 8, 1, 0, 2, 0)]
    public void Part2FindSidesTest(int i, int j, int di, int dj, int ee, int ii)
    {
        var data = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.".Split('\n');
        Assert.True(new Day22().FindSides(data, (i, j), (di, dj)) == (ee, ii));
    }

    [Theory]
    [InlineData(0, 8, -1, 0, true)]
    [InlineData(0, 8, 1, 0, false)]
    [InlineData(0, 8, 0, 1, false)]
    [InlineData(0, 8, 0, -1, true)]
    [InlineData(4, 2, 0, -1, false)]
    [InlineData(4, 2, 0, 1, false)]
    [InlineData(7, 0, -1, 0, false)]
    [InlineData(7, 0, 1, 0, true)]
    [InlineData(7, 0, 0, -1, true)]
    [InlineData(7, 0, 0, 1, false)]
    [InlineData(10, 8, -1, 0, false)]
    [InlineData(10, 8, 1, 0, false)]
    [InlineData(11, 8, -1, 0, false)]
    [InlineData(11, 8, 1, 0, true)]
    [InlineData(9, 15, -1, 0, false)]
    [InlineData(9, 15, 1, 0, false)]
    [InlineData(11, 15, 0, 1, true)]
    [InlineData(11, 12, 0, 1, false)]
    public void Part2IsNeedRotateTest(int i, int j, int di, int dj, bool isNeed)
    {
        var data = @"        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.".Split('\n');
        Assert.True(new Day22().IsNeedRotate(data, (i, j), (di, dj)) == isNeed);
    }
}
