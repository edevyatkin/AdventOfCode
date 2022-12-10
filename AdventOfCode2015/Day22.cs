using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,22)]
public class Day22 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var bossHp = int.Parse(input[0].Split(' ')[^1]);
        var bossDamage = int.Parse(input[1].Split(' ')[^1]);
        var max = (int)(1e9 + 7);
        var cache = new Dictionary<GameData, int>();
        var isPartTwo = false;
        
        int CalcMana(GameData data)
        {
            if (cache.ContainsKey(data))
                return cache[data];
            
            (int hp, int bossHp, int mana, int shield, int poison, int recharge, bool isPlayerTurn) = data;
            
            if (mana < 0)
                return cache[data] = max;

            if (isPartTwo && isPlayerTurn) hp -= 1;

            if (hp <= 0)
                return cache[data] = max;

            if (bossHp <= 0)
                return cache[data] = 0;

            var damage = 0;
            var armor = 0;

            if (shield > 0)
            {
                armor += 7;
                shield -= 1;
            }

            if (poison > 0)
            {
                damage += 3;
                poison -= 1;
            }

            if (recharge > 0)
            {
                mana += 101;
                recharge -= 1;
            }

            var newBossHp = bossHp - damage;

            if (newBossHp <= 0)
                return cache[data] = 0;

            if (!isPlayerTurn)
            {
                var newHp = hp - Math.Max(1, bossDamage - armor);
                return cache[data] = CalcMana(new(newHp, newBossHp, mana, shield, poison, recharge, true));
            }

            int Min(params int[] elems) => elems.Min();

            return cache[new GameData(hp, newBossHp, mana, shield, poison, recharge, isPlayerTurn)] = Min(
                53 + CalcMana(new(hp, newBossHp - 4, mana - 53, shield, poison, recharge, false)),
                73 + CalcMana(new(hp + 2, newBossHp - 2, mana - 73, shield, poison, recharge, false)),
                shield == 0 ? 113 + CalcMana(new(hp, newBossHp, mana - 113, 6, poison, recharge, false)) : max,
                poison == 0 ? 173 + CalcMana(new(hp, newBossHp, mana - 173, shield, 6, recharge, false)) : max,
                recharge == 0 ? 229 + CalcMana(new(hp, newBossHp, mana - 229, shield, poison, 5, false)) : max
            );
        }
        
        var result1 = CalcMana(new(50, bossHp, 500, 0, 0, 0, true));
        
        isPartTwo = true;
        cache.Clear();
        var result2 = CalcMana(new(50, bossHp, 500, 0, 0, 0, true));;
        
        return new AocDayResult(result1, result2);
    }

    private record GameData(int Hp, int BossHp, int Mana, int Shield, int Poison, int Recharge, bool IsPlayerTurn);
}
