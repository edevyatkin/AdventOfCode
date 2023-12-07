namespace AdventOfCode2023;

[AocDay(2023, 7)]
public class Day7 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart(input, false);
        var result2 = SolvePart(input, true);

        return new AocDayResult(result1, result2);
    }

    internal static int SolvePart(string[] input, bool isPartTwo) =>
        input.Select(c => {
                var sp = c.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return new Hand(sp[0], int.Parse(sp[1]), isPartTwo);
            })
            .OrderByDescending(cb => cb)
            .Select((c, i) => c.Bet * (i + 1)).Sum();

    private readonly struct Hand : IComparable<Hand>, IComparable
    {
        public string Cards { get; }
        public int Bet { get; }

        private readonly bool _isPartTwo = false;

        public Hand(string cards, int bet, bool isPartTwo)
        {
            _isPartTwo = isPartTwo;
            Cards = cards;
            Bet = bet;
        }

        int GetHandType(string h)
        {
            var counter = new Dictionary<char, int>();
            foreach (var c in h)
                counter[c] = counter.GetValueOrDefault(c, 0) + 1;

            var cardCounts = counter.Values.OrderBy(c => c).ToList();

            if (_isPartTwo)
            {
                var jokerCount = counter.GetValueOrDefault('J', 0);
                if (jokerCount is > 0 and < 5)
                {
                    cardCounts.Remove(jokerCount);
                    cardCounts[^1] += jokerCount;
                }
            }

            var type = -1;
            switch (cardCounts.Count)
            {
                case 1:
                    type = 0;
                    break;
                case 2:
                {
                    if (cardCounts[1] == 4)
                        type = 1;
                    if (cardCounts[0] == 2 && cardCounts[1] == 3)
                        type = 2;
                    break;
                }
                case 3:
                {
                    if (cardCounts[2] == 3)
                        type = 3;
                    if (cardCounts[1] == 2 || cardCounts[2] == 2)
                        type = 4;
                    break;
                }
                case 4:
                    type = 5;
                    break;
                default:
                    type = 6;
                    break;
            }

            return type;
        }

        public int CompareTo(Hand other)
        {
            var h1t = GetHandType(Cards);
            var h2t = GetHandType(other.Cards);

            if (h1t != h2t)
                return h1t < h2t ? -1 : 1;

            var cards = !_isPartTwo ? "AKQJT98765432" : "AKQT98765432J";
            for (int i = 0; i < Cards.Length; i++)
            {
                var ix1 = cards.IndexOf(Cards[i]);
                var ix2 = cards.IndexOf(other.Cards[i]);
                if (ix1 < ix2)
                    return -1;
                if (ix1 > ix2)
                    return 1;
            }

            return 0;
        }

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is Hand other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Hand)}");
        }
    }
}
