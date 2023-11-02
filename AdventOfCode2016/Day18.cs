using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,18)]
public class Day18 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var map = input[0];
        var result1 = CountSafeTiles(map, 40);
        var result2 = CountSafeTiles(map, 400_000);
        return new AocDayResult(result1, result2);
    }

    static bool IsTrap(string prevMap, int index)
    {
        var leftTrap = index > 0 && prevMap[index - 1] == '^';
        var centerTrap = prevMap[index] == '^';
        var rightTrap = index < prevMap.Length-1 && prevMap[index+1] == '^';
        var rule1 = leftTrap && centerTrap && !rightTrap;
        var rule2 = !leftTrap && centerTrap && rightTrap;
        var rule3 = leftTrap && !centerTrap && !rightTrap;
        var rule4 = !leftTrap && !centerTrap && rightTrap;
        return rule1 || rule2 || rule3 || rule4;
    }

    public static int CountSafeTiles(string map, int rows)
    {
        var result = 0;
        while (rows-- > 0)
        {
            result += map.Count(tile => tile == '.');
            var newMap = new StringBuilder();
            for (var i = 0; i < map.Length; i++)
                newMap.Append(IsTrap(map, i) ? '^' : '.');
            map = newMap.ToString();
        }
        return result;
    }
}
