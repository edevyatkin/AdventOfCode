using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var password = input[0];

        var result1 = GetNextPassword(password);
        var result2 = GetNextPassword(result1);

        return new AocDayResult(result1, result2);
    }

    private static string GetNextPassword(string password)
    {
        while (true)
        {
            var sb = new StringBuilder(password);
            while (sb[^1] == 'z')
                sb.Length--;
            sb[^1] = (char)(sb[^1] + 1);
            sb.Append('a', password.Length - sb.Length);
            password = sb.ToString();

            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
                continue;

            var i = 0;
            var foundTriplet = false;
            while (i < password.Length - 2)
            {
                if (password[i] == (char)(password[i + 1] - 1) && password[i] == (char)(password[i + 2] - 2))
                {
                    foundTriplet = true;
                    break;
                }

                i++;
            }

            if (!foundTriplet)
                continue;

            var pairs = 0;
            i = 0;
            while (i < password.Length - 1)
            {
                if (password[i] == password[i + 1])
                    pairs |= 1 << (password[i] - 'a');
                i++;
            }

            if ((pairs & (pairs - 1)) == 0)
                continue;

            break;
        }

        return password;
    }
}
