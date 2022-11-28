using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,13)]
public class Day13 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var happyDict = new Dictionary<(string A1, string A2), int>();
        var attendies = new List<string>();
        foreach (var s in input)
        {
            var sp = s.Split(' ');
            var attendee1 = sp[0];
            attendies.Add(attendee1);
            var attendee2 = sp[^1][..^1];
            var happiness = (sp[2] == "gain" ? 1 : -1) * int.Parse(sp[3]);
            happyDict[(attendee1, attendee2)] = happiness;
        }

        attendies = attendies.Distinct().ToList();

        int Arrange(List<string> arrange)
        {
            var maxHappiness = 0;
            if (arrange.Count == attendies.Count)
            {
                var happiness = 0;
                for (var i = 1; i < arrange.Count-1; i++)
                {
                    happiness += happyDict[(arrange[i], arrange[i-1])] + happyDict[(arrange[i], arrange[i+1])];
                }
                happiness += happyDict[(arrange[0], arrange[^1])] + happyDict[(arrange[0], arrange[1])];
                happiness += happyDict[(arrange[^1], arrange[^2])] + happyDict[(arrange[^1], arrange[0])];
                return happiness;
            }
            
            foreach (var attendee in attendies)
            {
                if (arrange.Contains(attendee))
                    continue;
                arrange.Add(attendee);
                maxHappiness = Math.Max(maxHappiness, Arrange(arrange));
                arrange.RemoveAt(arrange.Count-1);
            }

            return maxHappiness;
        }
        
        var result1 = Arrange(new List<string>());

        foreach (var attendee in attendies)
        {
            happyDict[(attendee, "Me")] = 0;
            happyDict[("Me", attendee)] = 0;
        }
        attendies.Add("Me");

        var result2 = Arrange(new List<string>());
        
        return new AocDayResult(result1, result2);
    }
}
