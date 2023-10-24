using System.Text;
using AdventOfCodeClient;

namespace AdventOfCode2016;

[AocDay(2016,4)]
public class Day4 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0;
        var result2 = 0;
        foreach (var room in input)
        {
            var checksum = room[^6..^1];
            var encryptedRoomName = room[..room.IndexOf('[')];
            var sectorId = int.Parse(encryptedRoomName[(encryptedRoomName.LastIndexOf('-')+1)..]);
            var count = new int[26];
            var ix = 0;
            while (!char.IsNumber(room[ix]))
            {
                if (room[ix] != '-')
                    count[room[ix] - 'a']++;
                ix++;
            }
            var sorted = count.Select((c, ci) => (-c, (char)(ci + 'a')))
                .OrderBy(p => p)
                .Select(e => e.Item2).ToArray();
            if (sorted[..5].SequenceEqual(checksum))
                result1 += sectorId;

            var roomName = DecryptRoomName(encryptedRoomName);
            if (roomName.Contains("northpole"))
            {
                result2 = sectorId;
            }
        }
        
        return new AocDayResult(result1, result2);
    }

    public static string DecryptRoomName(string encryptedRoomName)
    {
        var lastDash = encryptedRoomName.LastIndexOf('-');
        var sectorId = int.Parse(encryptedRoomName[(encryptedRoomName.LastIndexOf('-')+1)..]);
        var encName = encryptedRoomName[..lastDash];
        var realName = new StringBuilder();
        foreach (var ec in encName)
        {
            var rc = ec == '-' ? ' ' : (char)((ec - 'a' + sectorId % 26) % 26 + 'a');
            realName.Append(rc);
        }
        return realName.ToString();
    }
}
