using System.Diagnostics;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,21)]
public class Day21 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = int.MaxValue;
        var result2 = 0;
        
        var weapons = new List<Equipment>
        {
            new(8, 4, 0),
            new(10, 5, 0),
            new(25, 6, 0),
            new(40, 7, 0),
            new(74, 8, 0)
        };
        var armor = new List<Equipment>
        {
            new(13, 0, 1),
            new(31, 0, 2),
            new(53, 0, 3),
            new(75, 0, 4),
            new(102, 0, 5)
        };
        var rings = new List<Equipment>
        {
            new(25, 1, 0),
            new(50, 2, 0),
            new(100, 3, 0),
            new(20, 0, 1),
            new(40, 0, 2),
            new(80, 0, 3)
        };

        var bossHits = int.Parse(input[0].Split(": ")[1]);
        var bossDamage = int.Parse(input[1].Split(": ")[1]);
        var bossArmor = int.Parse(input[2].Split(": ")[1]);
        var bossEq = new Equipment(0, bossDamage, bossArmor);
        
        var myHits = 100;
        
        foreach (var w in SelectEquipment(weapons, 1,1))
        {
            foreach (var a in SelectEquipment(armor, 0, 1))
            {
                foreach (var r in SelectEquipment(rings, 0, 2))
                {
                    var myEq = w + a + r;
                    if (IsPlayerWin(bossHits, bossEq, myHits, myEq))
                    {
                        result1 = Math.Min(result1, myEq.Cost);
                    } 
                    else 
                    {
                        result2 = Math.Max(result2, myEq.Cost);
                    }
                }
            }
        }
        return new AocDayResult(result1, result2);
    }

    internal bool IsPlayerWin(int bossHits, Equipment bossEq, int myHits, Equipment myEq)
    {
        var myAttackDamage = Math.Max(1, myEq.Damage - bossEq.Armor);
        var myTurns = bossHits / myAttackDamage;
        if (bossHits % myAttackDamage != 0)
        {
            myTurns += 1;
        }
        var bossAttackDamage = Math.Max(1, bossEq.Damage - myEq.Armor);
        var bossTurns = myHits / bossAttackDamage;
        if (myHits % bossAttackDamage != 0)
        {
            bossTurns += 1;
        }

        return myTurns <= bossTurns;
    }

    IEnumerable<Equipment> SelectEquipment(List<Equipment> equipment, int min, int max)
    {
        if (min < 0 || min > max)
        {
            throw new ArgumentException("Min must be greater than or equal to 0 and less than or equal to max", nameof(min));
        }
        if (max > equipment.Count)
        {
            throw new ArgumentException("Max must be less than or equal to the amount of equipment", nameof(max));
        }

        for (var i = 0; i < (1<<equipment.Count); i++)
        {
            var bits = 0;
            for (var n = i; n > 0; n >>= 1)
            {
                if ((n & 1) == 1)
                {
                    bits++;
                }
            }

            if (bits < min || bits > max)
            {
                continue;
            }

            var eq = new Equipment();
            for (int n = i, bit = 0; n > 0; n >>= 1, bit++)
            {
                if ((n & 1) == 1)
                {
                    eq += equipment[bit];
                }
            }

            yield return eq;
        }
    }

    [DebuggerDisplay("[{Cost},{Damage},{Armor}]")]
    internal struct Equipment
    {
        public int Cost { get; init; }
        public int Damage { get; init; }
        public int Armor { get; init; }

        public Equipment(int cost, int damage, int armor)
        {
            Cost = cost;
            Damage = damage;
            Armor = armor;
        }

        public static Equipment operator +(Equipment e1, Equipment e2)
        {
            return new Equipment(e1.Cost + e2.Cost, e1.Damage + e2.Damage, e1.Armor + e2.Armor);
        }
    }
}
