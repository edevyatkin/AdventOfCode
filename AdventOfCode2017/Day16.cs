using AdventOfCodeClient;

namespace AdventOfCode2017;

[AocDay(2017,16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = Dance(input[0], 16, 1);
        var result2 = Dance(input[0], 16, 1_000_000_000);;
        
        return new AocDayResult(result1, result2);
    }

    public static string Dance(string moves, int programsCount, int times)
    {
        var progs = Enumerable.Range('a', programsCount)
            .Select(i => (char)i)
            .ToArray();
        
        var t = 0;
        var cache = new Dictionary<string, int>();
        var result = string.Empty;
        var startIndex = 0;

        while (t < times)
        {
            foreach (var moveStr in moves.Split(','))
            {
                var (move, data) = (moveStr[0], moveStr[1..]);
                switch (move)
                {
                    case 's':
                        var spin = int.Parse(data);
                        startIndex = (startIndex + programsCount - spin) % programsCount;
                        break;
                    case 'x':
                        var posArr = data.Split('/')
                            .Select(p => (startIndex + int.Parse(p)) % programsCount).ToArray();
                        (progs[posArr[0]], progs[posArr[1]]) = (progs[posArr[1]], progs[posArr[0]]);
                        break;
                    case 'p':
                        var namesArr = data.Split('/');
                        var pos1 = Array.IndexOf(progs, namesArr[0][0]);
                        var pos2 = Array.IndexOf(progs, namesArr[1][0]);
                        (progs[pos1], progs[pos2]) = (progs[pos2], progs[pos1]);
                        break;
                }
            }
            
            var buffer = new char[programsCount];
            progs[startIndex..].CopyTo(buffer, 0);
            progs[..startIndex].CopyTo(buffer, programsCount - startIndex);
        
            result = new string(buffer);
            if (cache.TryGetValue(result, out var prev))
            {
                times %= t - prev;
                t = 0;
                cache.Clear();
            }
            else
            {
                cache[result] = t;
            }
            
            t++;
        }

        return result;
    }
}
