using System.Numerics;
using AdventOfCodeClient;
using Microsoft.Z3;

namespace AdventOfCode2025;

[AocDay(2025, 10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0L;

        foreach (var line in input)
        {
            var sp = line.Split();
            var indicators = 0;
            for (var i = 1; i < sp[0].Length - 1; i++)
                indicators |= (sp[0][i] == '#' ? (1 << (i - 1)) : 0);

            var buttons = sp[1..^1]
                .Select(bs => bs[1..^1].Split(',')
                .Aggregate(0, (a, s) => a | (1 << (s[0] - '0'))))
                .ToArray();

            var minPresses = int.MaxValue;
            for (var combination = 1; combination < (1 << buttons.Length); combination++)
            {
                var currentCombination = 0;
                for (var bit = 0; bit < 32; bit++)
                    if ((combination & (1 << bit)) > 0)
                        currentCombination ^= buttons[bit];

                if (currentCombination == indicators)
                    minPresses = Math.Min(minPresses, BitOperations.PopCount((uint)combination));
            }

            result1 += minPresses;

            var voltages = sp[^1][1..^1].Split(',').Select(int.Parse).ToArray();
            using var solver = new VoltageSolver();
            result2 += solver.Solve(buttons.ToArray(), voltages);
        }

        return new AocDayResult(result1, result2);
    }

    public class VoltageSolver : IDisposable
    {
        private readonly Context _context;
        private readonly Optimize _optimize;

        public VoltageSolver()
        {
            _context = new Context();
            _optimize = _context.MkOptimize();
        }

        public long Solve(int[] buttons, int[] voltages)
        {
            var consts = Enumerable.Range(0, buttons.Length)
                .Select(d => _context.MkIntConst(char.ToString((char)('a' + d)))).ToArray();

            foreach (var intExpr in consts)
                _optimize.Add(_context.MkGe(intExpr, _context.MkInt(0)));

            for (var vi = 0; vi < voltages.Length; vi++)
            {
                var constsToAdd = new List<IntExpr>();
                for (var bi = 0; bi < buttons.Length; bi++)
                    if ((buttons[bi] & (1 << vi)) > 0)
                        constsToAdd.Add(consts[bi]);
                _optimize.Add(_context.MkEq(_context.MkAdd(constsToAdd), _context.MkInt(voltages[vi])));
            }

            var handle = _optimize.MkMinimize(_context.MkAdd(consts));

            if (_optimize.Check() != Status.SATISFIABLE)
                throw new Exception("Optimize failed");

            return long.Parse(handle.Value.ToString());
        }

        public void Dispose()
        {
            _context.Dispose();
            _optimize.Dispose();
        }
    }
}
