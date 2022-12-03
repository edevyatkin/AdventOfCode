using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,20)]
public class Day20 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;

        var presents = int.Parse(input[0]);
        
        // PART 1
        
        var lowestHouse = 0;
        var sum = 0;
        var counts = new Dictionary<int, int>();

        while (sum < presents)
        {
            sum = SumForHouses(++lowestHouse, counts, false);
        }
        
        result1 = lowestHouse;
        
        // PART 2

        lowestHouse = 0;
        sum = 0;
        while (sum < presents)
        {
            sum = SumForHouses(++lowestHouse, counts, true);
        }

        result2 = lowestHouse;
        
        return new AocDayResult(result1, result2);
    }

    private int SumForHouses(int lowestHouse, Dictionary<int,int> counts, bool isPartTwo)
    {
        var sum = 0;
        for (var i = 1; i * i <= lowestHouse; i++)
        {
            if (lowestHouse % i != 0) continue;
            sum += SumDivisor(i, counts, isPartTwo);
            if (lowestHouse / i != i)
            {
                sum += SumDivisor(lowestHouse/i, counts, isPartTwo);
            }
        }

        return sum;
    }

    int SumDivisor(int divisor, Dictionary<int,int> counts, bool isPartTwo)
    {
        if (!isPartTwo)
        {
            return divisor * 10;
        }

        counts[divisor] = counts.GetValueOrDefault(divisor) + 1;
        if (counts[divisor] <= 50)
        {
            return divisor * 11;
        }

        return 0;
    }
}
