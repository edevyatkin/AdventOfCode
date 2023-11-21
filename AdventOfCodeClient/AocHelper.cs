using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCodeClient;

public static class AocHelper {
    private const string SessionKeyFile = "session_key";

    public static async Task<string[]> FetchInputAsync(int year, int day)
    {
        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var sessionKeyFullPath = Path.Combine(path!, SessionKeyFile);
        if (!File.Exists(sessionKeyFullPath))
            throw new FileNotFoundException("Please create a 'session_key' file with your AoC key into AdventOfCodeClient project folder. " +
                                            "Your can get the session key from cookies of adventofcode.com site after logging in.");
        
        var sessionKey = await File.ReadAllTextAsync(sessionKeyFullPath);
        var aocClient = new AocClient(sessionKey.Trim());
        return await aocClient.FetchInputAsync(year, day);
    }
}
