using System.Numerics;
using AdventOfCodeClient;

namespace AdventOfCode2022;

[AocDay(2022,11)]
public class Day11 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var gamePart1 = new Game(false);
        var gamePart2 = new Game(true);
        
        for (var i = 1; i < input.Length; i += 7)
        {
            var items = input[i].Split(": ")[1].Split(", ").Select(long.Parse).ToList();
            var opStr = input[i + 1].Split(" = ")[1].Split();
            Func<long,long> operation = opStr[1] switch
            {
                "+" => long.TryParse(opStr[2], out var val) ? x => x + val : x => x + x,
                "*" => long.TryParse(opStr[2], out var val) ? x => x * val : x => x * x
            };
            var divisible = int.Parse(input[i + 2].Split()[^1]);
            var monkeyIfTrue = int.Parse(input[i + 3].Split()[^1]); 
            var monkeyIfFalse = int.Parse(input[i + 4].Split()[^1]);
            gamePart1.AddMonkey(items, operation, divisible, monkeyIfTrue, monkeyIfFalse);
            gamePart2.AddMonkey(items, operation, divisible, monkeyIfTrue, monkeyIfFalse);
        }

        var rounds1 = 20;
        while (--rounds1 >= 0)
        {
            gamePart1.Round();
        }
        var result1 = gamePart1.GetMonkeyBusiness();
        
        var rounds2 = 10000;
        while (--rounds2 >= 0)
        {
            gamePart2.Round();
        }
        var result2 = gamePart2.GetMonkeyBusiness();
        
        return new AocDayResult(result1, result2);
    }

    private class Game
    {
        private readonly List<Monkey> _monkeys = new();
        public long Mod { get; private set; } = 1;
        public bool IsPartTwo { get; }

        public Game(bool isPartTwo)
        {
            IsPartTwo = isPartTwo;
        }
        
        public void AddMonkey(List<long> items, Func<long, long> operation, int divisible, int monkeyIfTrue,
            int monkeyIfFalse)
        {
            _monkeys.Add(new Monkey(items, operation, new Test(divisible, monkeyIfTrue, monkeyIfFalse), this));
            Mod *= divisible;
        }

        public void Round()
        {
            foreach (var monkey in _monkeys)
            {
                foreach (var item in monkey.Items)
                {
                    var result = monkey.Inspect(item);
                    _monkeys[result.ToMonkey].Items.Add(result.WorryLevel);
                }
                monkey.Items.Clear();
            }
        }

        public long GetMonkeyBusiness() =>
            _monkeys.Select(m => m.Inspections)
                .OrderByDescending(i => i)
                .Take(2)
                .Aggregate((x, y) => x * y);
    }
    
    record Test(int Divisible, int MonkeyIfTrue, int MonkeyIfFalse);
    
    class Monkey
    {
        private readonly Func<long, long> _operation;
        private readonly Test _test;
        private readonly Game _game;
        
        public List<long> Items { get; }
        public long Inspections { get; private set; }

        public Monkey(List<long> items, Func<long, long> operation, Test test, Game game)
        {
            Items = items.ToList();
            _operation = operation;
            _test = test;
            _game = game;
        }

        public (int ToMonkey, long WorryLevel) Inspect(long item)
        {
            var worryLevel = _operation(item);
            if (!_game.IsPartTwo)
            {
                worryLevel /= 3;
            }
            else
            {
                worryLevel %= _game.Mod;
            }
            var toMonkey = worryLevel % _test.Divisible == 0 ? _test.MonkeyIfTrue : _test.MonkeyIfFalse;
            Inspections++;
            return (toMonkey, worryLevel);
        }
    }
}


