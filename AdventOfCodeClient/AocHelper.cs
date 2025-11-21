using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeClient;

public static class AocHelper {
    private const string SessionKeyFile = "session_key";

    public static async Task<string[]> FetchInputAsync(int year, int day)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var sessionKeyFullPath = Path.Combine(path!, SessionKeyFile);
        if (!File.Exists(sessionKeyFullPath))
        {
            var key = string.Empty;
            while (true)
            {
                Console.Write("Please insert here a 'session' from cookies of adventofcode.com site after logging in : ");
                key = Console.ReadLine().Trim();
                if (!Regex.IsMatch(key, "^[a-z0-9]"))
                {
                    Console.WriteLine("Invalid session key");
                    continue;
                }
                break;
            }
            await File.WriteAllTextAsync(sessionKeyFullPath, key);
        }
        var sessionKey = await File.ReadAllTextAsync(sessionKeyFullPath);
        var aocClient = new AocClient(sessionKey.Trim());
        return await aocClient.FetchInputAsync(year, day);
    }
}
