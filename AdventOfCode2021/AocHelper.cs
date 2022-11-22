using System.Threading.Tasks;

namespace AdventOfCode2021;

public class AocHelper
{
    public static async Task<string[]> FetchInputAsync(int year, int day)
    {
        return await AdventOfCodeClient.AocHelper.FetchInputAsync(year, day);
    }
}