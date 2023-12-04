namespace AdventOfCode2023;

[AocDay(2023,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        
        var counts = new int[input.Length+1];
        Array.Fill(counts, 1);
        counts[0] = 0;
        
        for (var cardNum = 1; cardNum < input.Length; cardNum++)
        {
            var card = input[cardNum-1];
            var sp = card.Split(" | ");
            var winning = sp[0].Split(": ")[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToHashSet();
            var have = sp[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            
            var matches = have.Count(n => winning.Contains(n));
            
            result1 += (int)Math.Pow(2, matches - 1);
            
            for (var diff = 1; diff <= matches; diff++)
                counts[cardNum + diff] += counts[cardNum];
        }

        var result2 = counts.Sum();
        
        return new AocDayResult(result1, result2);
    }
}
