using System.Security.Cryptography;
using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,17)]
public class Day17 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = string.Empty;
        var result2 = 0;
        var pq = new PriorityQueue<(int I, int J, string CodeAndPath), (int,int)>();
        pq.Enqueue((0,0, input[0]), (input[0].Length, 6));
        (char Dir, (int Di, int Dj) Diff)[] dirs = {
            ('U', (-1, 0)),
            ('D', (1, 0)),
            ('L', (0, -1)),
            ('R', (0, 1))
        };
        while (pq.Count > 0)
        {
            var (i, j, cp) = pq.Dequeue();
            var doorSet = GetDoorSet(cp);
            if (i == 3 && j == 3)
            {
                if (result1 == string.Empty)
                    result1 = cp[input[0].Length..];
                var pathLen = cp.Length - input[0].Length;
                if (pathLen > result2)
                    result2 = pathLen;
                continue;
            }
            for (int shift = 0; shift < 4; shift++)
            {
                var dir = dirs[shift];
                var ni = i + dir.Diff.Di;
                var nj = j + dir.Diff.Dj;
                var doorIsOpen = (doorSet & (1 << (3 - shift))) > 0;
                if (ni < 0 || ni == 4 || nj < 0 || nj == 4 || !doorIsOpen)
                    continue;
                var ncd = cp + dir.Dir;
                pq.Enqueue((ni,nj,ncd), (ncd.Length, 6 - ni - nj));
            }
        }
        return new AocDayResult(result1, result2);
    }

    public static int GetDoorSet(string passcodeAndPath)
    {
        var md5 = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(passcodeAndPath))).ToLowerInvariant();
        var doorSet = 0;
        for (var i = 0; i < 4; i++)
            if (md5[i] != 'a' && !char.IsDigit(md5[i]))
                doorSet |= (1 << (3 - i));
        return doorSet;
    }
}
