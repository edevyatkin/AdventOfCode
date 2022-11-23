using System.Threading.Tasks;

namespace AdventOfCodeClient;

public interface IAocDay
{
    Task<AocDayResult> Solve(int year, int day);
}
