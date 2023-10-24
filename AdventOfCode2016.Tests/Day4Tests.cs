using Xunit;

namespace AdventOfCode2016.Tests;

public class Day4Tests
{
    [Theory]
    [InlineData(new[]
    {
        "aaaaa-bbb-z-y-x-123[abxyz]",
        "a-b-c-d-e-f-g-h-987[abcde]",
        "not-a-real-room-404[oarel]",
        "totally-real-room-200[decoy]"
    }, 1514)]
    public void Part1Test(string[] rooms, int result)
    {
        Assert.Equal(result, new Day4().Solve(rooms).Part1);
    }
    
    [Theory]
    [InlineData("qzmt-zixmtkozy-ivhz-343", "very encrypted name")]
    public void Part2ShiftCipherTest(string room, string result)
    {
        Assert.Equal(result, Day4.DecryptRoomName(room));
    }
}
