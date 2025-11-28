using System.Text.RegularExpressions;
using AdventOfCodeClient;

namespace AdventOfCode2019;

[AocDay(2019, 12)]
public class Day12 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input, 1000);
        var result2 = 0;
        return new AocDayResult(result1, result2);
    }

    private static long CalcTotalEnergy(Moon[] moons) =>
        moons.Aggregate(0L,
            (s, m) => s + (Math.Abs(m.Px) + Math.Abs(m.Py) + Math.Abs(m.Pz)) *
                (Math.Abs(m.Vx) + Math.Abs(m.Vy) + Math.Abs(m.Vz)));

    private static void Gravity(Moon[] moons)
    {
        for (var i = 0; i < moons.Length; i++)
        {
            for (var j = i + 1; j < moons.Length; j++)
            {
                if (moons[i].Px < moons[j].Px)
                {
                    moons[i].Vx += 1;
                    moons[j].Vx -= 1;
                } 
                else if (moons[i].Px > moons[j].Px)
                {
                    moons[i].Vx -= 1;
                    moons[j].Vx += 1;
                }
                
                if (moons[i].Py < moons[j].Py)
                {
                    moons[i].Vy += 1;
                    moons[j].Vy -= 1;
                } 
                else if (moons[i].Py > moons[j].Py)
                {
                    moons[i].Vy -= 1;
                    moons[j].Vy += 1;
                }
                
                if (moons[i].Pz < moons[j].Pz)
                {
                    moons[i].Vz += 1;
                    moons[j].Vz -= 1;
                } 
                else if (moons[i].Pz > moons[j].Pz)
                {
                    moons[i].Vz -= 1;
                    moons[j].Vz += 1;
                }
            }
        }
    }

    public long SolvePart1(string[] input, int steps)
    {
        var moons = new Moon[input.Length];
        for (var index = 0; index < input.Length; index++)
        {
            var s = input[index];
            var match = Regex.Match(s, @"<x=(-?\d+), y=(-?\d+), z=(-?\d+)>");
            moons[index] = new Moon {
                Px = int.Parse(match.Groups[1].Value),
                Py = int.Parse(match.Groups[2].Value),
                Pz = int.Parse(match.Groups[3].Value)
            };
        }

        while (steps-- > 0)
        {
            Gravity(moons);
            for (var index = 0; index < moons.Length; index++)
            {
                moons[index].Px += moons[index].Vx;
                moons[index].Py += moons[index].Vy;
                moons[index].Pz += moons[index].Vz;
            }
        }
        return CalcTotalEnergy(moons);
    }

    private struct Moon
    {
        public int Px { get; set; }
        public int Py { get; set; }
        public int Pz { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }
        public int Vz { get; set; }

        public override string ToString()
        {
            return $"pos=<x={Px}, y={Py}, z={Pz}>, vel=<x={Vx}, y={Vy}, z={Vz}>";
        }
    }
}


