using System.IO;
using System.Threading.Tasks;

namespace AdventOfCodeClient {
    public static class AocHelper {
        private const string SessionKeyFile = "session_key";

        public static async Task<string[]> FetchInputAsync(int year, int day) {
            var sessionKey = await File.ReadAllTextAsync(SessionKeyFile);
            var aocClient = new AocClient(sessionKey.Trim());
            return await aocClient.FetchInputAsync(year, day);
        }
    }
}