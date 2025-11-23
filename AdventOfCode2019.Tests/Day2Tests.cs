namespace AdventOfCode2019.Tests;

public class Day2Tests
{
    [Theory]
    [InlineData(new [] { 1,9,10,3,2,3,11,0,99,30,40,50 }, 3500)]
    [InlineData(new [] { 1,0,0,0,99 }, 2)]
    [InlineData(new [] { 2,3,0,3,99 }, 2)]
    [InlineData(new [] { 2,4,4,5,99,0 }, 2)]
    [InlineData(new [] { 1,1,1,4,99,5,6,0,99 }, 30)]
    public void Part1Test(int[] nums, int result)
    {
        Assert.Equal(result, new Day2().Run(nums, nums[1], nums[2]));
    }
}
