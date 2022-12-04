using Xunit;

namespace AdventOfCode2015.Tests;

public class Day21Tests
{
    [Theory]
    [InlineData(12,7,2, 8,5,5, true)]
    public void IsPlayerWinTest(int bossHits, int bossDamage, int bossArmor, int myHits, int myDamage, int myArmor, bool result)
    {
        Assert.Equal(result, 
            new Day21().IsPlayerWin(bossHits, new Day21.Equipment(0, bossDamage, bossArmor), 
                                         myHits, new Day21.Equipment(0, myDamage, myArmor)));
    }
}
