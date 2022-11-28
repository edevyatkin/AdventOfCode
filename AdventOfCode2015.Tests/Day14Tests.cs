using Xunit;

namespace AdventOfCode2015.Tests;

public class Day14Tests
{
    [Theory]
    [InlineData(new[]
    {
        "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
        "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
    }, 1000, 1120)]
    public void Part1Test(string[] reindeers, int time, int result)
    {
        Assert.Equal(result, new Day14().GetWinningDistance(reindeers, time));
    }
    
    [Theory]
    [InlineData(new[]
    {
        "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.",
        "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds."
    }, 1000, 689)]
    public void Part2Test(string[] reindeers, int time, int result)
    {
        Assert.Equal(result, new Day14().GetWinningPoints(reindeers, time));
    }
}
