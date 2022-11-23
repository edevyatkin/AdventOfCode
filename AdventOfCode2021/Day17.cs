using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,17)]
public class Day17 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        var sp = input[0].Split(' ')[2..];
        var xCoord = sp[0][2..^1].Split("..").Select(int.Parse).ToArray();
        var yCoord = sp[1][2..].Split("..").Select(int.Parse).ToArray();

        // PART 1
        int val = -(yCoord[0] + 1);
        int result1 = val * (val + 1) / 2;

        // PART 2
        int result2 = 0;
        for (int iv = 0; iv <= xCoord[1]; iv++) {
            for (int jv = yCoord[0]; jv <= -(yCoord[0]+1); jv++) {
                int curIv = iv;
                int curJv = jv;
                int i = 0;
                int j = 0;
                while (i <= xCoord[1] && j >= yCoord[0]) {
                    if (i >= xCoord[0] && i <= xCoord[1] && j >= yCoord[0] && j <= yCoord[1]) {
                        result2++;
                        break;
                    }
                    i += curIv;
                    j += curJv;
                    if (curIv > 0)
                        curIv--;
                    curJv--;
                }
            }
        }

        return new AocDayResult(result1, result2);
    }
}