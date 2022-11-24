using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,5)]
public class Day5 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        
        foreach (var s in input)
        {
            result1 += IsNiceString(s);
            result2 += IsNiceString2(s);
        }

        return new AocDayResult(result1, result2);
    }

    private int IsNiceString(string s)
    {
        var containThreeVowels = false;
        var twoLettersInARow = false;
        var containStrs = false;

        var vowels = "aeiou";
        var vowelsCount = 0;
        var strs = "acpx";

        for (var i = 0; i < s.Length; i++)
        {
            if (i < s.Length - 1 && s[i] == s[i + 1])
                twoLettersInARow = true;
            if (vowels.Contains(s[i]) && ++vowelsCount >= 3)
                containThreeVowels = true;
            if (i < s.Length - 1 && strs.Contains(s[i]) && s[i + 1] == (char)(s[i] + 1))
                containStrs = true;
        }

        return containThreeVowels && twoLettersInARow && !containStrs ? 1 : 0;
    }

    private int IsNiceString2(string s)
    {
        var hasPair = false;
        var dict = new Dictionary<string, List<int>>();
        var hasLetter = false;

        for (int i = 0; i < s.Length; i++)
        {
            if (i < s.Length - 1)
            {
                var pair = s[i..(i + 2)];
                if (!dict.ContainsKey(pair))
                    dict[pair] = new List<int>();
                dict[pair].Add(i);
                if (dict[pair].Count > 1 && (dict[pair].Max() - dict[pair].Min() > 1))
                    hasPair = true;
            }
            if (i < s.Length - 2 && s[i] == s[i + 2])
                hasLetter = true;
        }
        
        return hasPair && hasLetter ? 1 : 0;
    }
}