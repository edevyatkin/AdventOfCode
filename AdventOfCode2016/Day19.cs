using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,19)]
public class Day19 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var elvesCount = int.Parse(input[0]);
        var result1 = GetElfNumber(elvesCount, false);
        var result2 = GetElfNumber(elvesCount, true);
        return new AocDayResult(result1, result2);
    }
    
    public static int GetElfNumber(int elvesCount, bool IsPartTwo)
    {
        var list = new LinkedList<int>(Enumerable.Range(1, elvesCount));
        var current = list.First;
        var toStealNum = !IsPartTwo ? 2 : 1 + elvesCount / 2;
        var next = current;
        while (next.Value != toStealNum)
            next = next.Next;
        while (list.Count > 1)
        {
            var newNext = next.Next ?? list.First;
            list.Remove(next);
            current = current.Next ?? list.First;
            if (!IsPartTwo || list.Count % 2 == 0)
                newNext = newNext.Next ?? list.First;
            next = newNext;
        }
        return list.First.Value;
    }
}
