using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        const string word = "XMAS";
        var diffs = new List<(int, int)> { (0, 1), (1, 0), (-1, 0), (0, -1), (-1, -1), (-1, 1), (1, 1), (1, -1) };
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[0].Length; j++)
            {
                foreach (var (di, dj) in diffs)
                {
                    var wi = i;
                    var wj = j;
                    var foundWord = true;
                    for (var k = 0; k < word.Length; k++)
                    {
                        if (k > 0)
                        {
                            wi += di;
                            wj += dj;
                        }
                        if (wi < 0 || wi == input.Length || wj < 0 || wj == input[0].Length || input[wi][wj] != word[k])
                        {
                            foundWord = false;
                            break;
                        }
                    }
                    if (!foundWord)
                        continue;
                    result1++;
                }
            }
        }

        var lettersInDiagonals = new List<string> {
            "SSMM", "SMSM", "MSMS", "MMSS"
        };

        for (var i = 1; i < input.Length-1; i++)
            for (var j = 1; j < input[0].Length-1; j++)
                if (input[i][j] == 'A')
                    foreach (var letterStr in lettersInDiagonals)
                        if (input[i-1][j-1] == letterStr[0] && 
                            input[i-1][j+1] == letterStr[1] && 
                            input[i+1][j-1] == letterStr[2] && 
                            input[i+1][j+1] == letterStr[3])
                            result2++;

        return new AocDayResult(result1, result2);
    }
}
