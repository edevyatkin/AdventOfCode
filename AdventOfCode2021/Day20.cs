using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,20)]
public class Day20 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);
        
        int GetResult(string[] strings, int steps)
        {
            var algorithm = strings[0].Select(e => e == '#').ToArray();

            int areaH = 210;
            int areaW = 210;
            bool[][] image = new bool[areaH][];
            for (int i = 0; i < areaH; i++)
            {
                image[i] = new bool[areaW];
            }

            int h = strings.Length;
            int w = strings[2].Length;

            int tlI = (areaH - 1) / 2 - (h - 1) / 2 - 1;
            int tlJ = (areaW - 1) / 2 - ((w - 1) - 2) / 2 - 1;

            for (int i = 2; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    int ix = i + tlI;
                    int jx = j + tlJ;
                    image[ix][jx] = strings[i][j] == '#';
                }
            }

            
            int result = 0;
            var diffs = new List<(int, int)>()
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 0), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };
            var overFlowOne = 0;
            int step = 1;
            while (step <= steps)
            {
                bool[][] image2 = new bool[areaH][];
                for (int i = 0; i < areaH; i++)
                {
                    image2[i] = new bool[areaW];
                }

                for (int i = 0; i < image.Length; i++)
                {
                    for (int j = 0; j < image[0].Length; j++)
                    {
                        int algIndex = 0;
                        foreach (var diff in diffs)
                        {
                            int ni = i + diff.Item1;
                            int nj = j + diff.Item2;
                            algIndex <<= 1;
                            if ((ni < 0 || ni == image.Length || nj < 0 || nj == image[0].Length))
                            {
                                algIndex |= overFlowOne;
                                continue;
                            }

                            var algIndexVal = image[ni][nj] ? 1 : 0;
                            algIndex |= algIndexVal;
                        }

                        image2[i][j] = algorithm[algIndex];
                    }
                }

                image = image2;
                if (algorithm[0])
                    overFlowOne ^= 1;
                step++;
            }

            for (var i = 0; i < image.Length; i++)
            {
                for (int j = 0; j < image[0].Length; j++)
                {
                    if (image[i][j])
                        result++;
                }
            }

            return result;
        }
        
        // PART 1
        var result1 = GetResult(input, 2);
        
        // PART 2
        var result2 = GetResult(input, 50);

        return new AocDayResult(result1, result2);
    }
}