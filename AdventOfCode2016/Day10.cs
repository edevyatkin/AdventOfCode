using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,10)]
public class Day10 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = SolvePart1(input, 61, 17);
        var result2 = SolvePart2(input);
        return new AocDayResult(result1, result2);
    }

    public int SolvePart1(string[] input, int v1, int v2)
    {
        var (comparingHistory, bins) = Proceed(input);
        return comparingHistory.Find(e => IsComparingValues(e, v1, v2)).BotId;
    }
    
    public int SolvePart2(string[] input)
    {
        var (comparingHistory, bins) = Proceed(input);
        return bins[0] * bins[1] * bins[2];
    }
    
    static Dictionary<int, Bot> ParseInput(string[] input)
    {
        var bots = new Dictionary<int, Bot>();;
        foreach (var s in input)
        {
            var sp = s.Split();
            if (sp[0] == "value")
            {
                var value = int.Parse(sp[1]);
                var botId = int.Parse(sp[^1]);
                bots.TryAdd(botId, new(botId));
                bots[botId].AddValue(value);
            }
            else
            {
                var botId = int.Parse(sp[1]);
                var lowId = int.Parse(sp[6]);
                var highId = int.Parse(sp[^1]);
                bots.TryAdd(botId, new(botId));
                if (sp[5] == "bot")
                    bots[botId].Bot1 = lowId;
                else
                    bots[botId].Output1 = lowId;
                if (sp[^2] == "bot")
                    bots[botId].Bot2 = highId;
                else
                    bots[botId].Output2 = highId;
            }
        }

        return bots;
    }
    
    bool IsComparingValues((int,int,int) historyItem, int v1, int v2) => 
        v1 == historyItem.Item2 && v2 == historyItem.Item3 || v1 == historyItem.Item3 && v2 == historyItem.Item2;

    ProceedData Proceed(string[] input)
    {
        var bots = ParseInput(input);
        var bins = new Dictionary<int, int>();
        var comparingHistory = new List<(int BotId, int Value1, int Value2)>();
        var q = new Queue<Bot>();
        foreach (var b in bots)
        {
            if (b.Value.CanProceed())
                q.Enqueue(b.Value);
        }
        while (q.Count > 0)
        {
            var bot = q.Dequeue();
            comparingHistory.Add((bot.Id, bot.Value1, bot.Value2));
            var minValue = bot.Value1 < bot.Value2 ? bot.Value1 : bot.Value2;
            var maxValue = bot.Value1 < bot.Value2 ? bot.Value2 : bot.Value1;
            if (bot.Bot1 != -1)
            {
                var bot1 = bots[bot.Bot1];
                bot1.AddValue(minValue);
                bot.RemoveValue(minValue);
                if (bot1.CanProceed())
                    q.Enqueue(bot1);
            }
            if (bot.Bot2 != -1)
            {
                var bot2 = bots[bot.Bot2];
                bot2.AddValue(maxValue);
                bot.RemoveValue(maxValue);
                if (bot2.CanProceed())
                    q.Enqueue(bot2);
            }
            if (bot.Output1 != -1)
            {
                bins[bot.Output1] = minValue;
                bot.RemoveValue(minValue);
            }
            if (bot.Output2 != -1)
            {
                bins[bot.Output2] = maxValue;
                bot.RemoveValue(maxValue);
            }
        }

        return new ProceedData(comparingHistory, bins);
    }
    
    record ProceedData(List<(int BotId, int Value1, int Value2)> ComparingHistory, Dictionary<int,int> Bins);

    class Bot
    {
        public int Id { get; }
        public int Value1 { get; set; } = -1;
        public int Value2 { get; set; } = -1;
        public int Bot1 { get; set; } = -1; // for low
        public int Bot2 { get; set; } = -1; // for high
        public int Output1 { get; set; } = -1; // for low
        public int Output2 { get; set; } = -1; // for high

        public Bot(int botId)
        {
            Id = botId;
        }
        
        public bool CanProceed() => Value1 != -1 && Value2 != -1;

        public void AddValue(int value)
        {
            if (Value1 == -1)
                Value1 = value;
            else if (Value2 == -1)
                Value2 = value;
        }

        public void RemoveValue(int value)
        {
            if (Value1 == value)
                Value1 = -1;
            else if (Value2 == value)
                Value2 = -1;
        }

        public override string ToString()
        {
            return $"{Id} ({Value1} {Value2})";
        }
    }
    
}

