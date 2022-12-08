using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022, 8)]
public class Day8 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var rows = input.Length;
        var cols = input[0].Length;

        bool IsVisible(int i, int j)
        {
            var height = input[i][j];
            int t = i - 1;
            while (t >= 0 && input[t][j] < height) t--;
            if (t < 0) return true;
            int r = j + 1;
            while (r < cols && input[i][r] < height) r++;
            if (r == cols) return true;
            int b = i + 1;
            while (b < rows && input[b][j] < height) b++;
            if (b == rows) return true;
            int l = j - 1;
            while (l >= 0 && input[i][l] < height) l--;
            if (l < 0) return true;
            return false;
        }

        int CalculateScenicScore(int i, int j)
        {
            var res = 1;
            var height = input[i][j];
            
            if (i > 0)
            {
                int t = i - 1;
                while (t > 0 && input[t][j] < height) t--;
                res *= i - t;
            }

            if (j < cols)
            {
                int r = j + 1;
                while (r < cols - 1 && input[i][r] < height) r++;
                res *= j - r;
            }

            if (i < rows)
            {
                int b = i + 1;
                while (b < rows - 1 && input[b][j] < height) b++;
                res *= i - b;
            }

            if (j > 0)
            {
                int l = j - 1;
                while (l > 0 && input[i][l] < height) l--;
                res *= j - l;
            }

            return res;
        }

        var hs = new HashSet<(int, int)>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (IsVisible(i, j))
                {
                    hs.Add((i, j));
                }
                result2 = Math.Max(result2, CalculateScenicScore(i, j));
            }
        }

        result1 = hs.Count;

        return new AocDayResult(result1, result2);
    }
}
