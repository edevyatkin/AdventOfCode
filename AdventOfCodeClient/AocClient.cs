using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeClient {
    public class AocClient {
        private readonly string _session;
        private static readonly Lazy<HttpClient> Client = new();

        public AocClient(string session) {
            if (!Regex.IsMatch(session, "[0-9a-z]{96}"))
                throw new ArgumentException($"Wrong session key: {session}", nameof(session));
            _session = session;
        }
        
        public async Task<string[]> FetchInputAsync(int year, int day) {
            if (year is < 2015 or > 2021)
                throw new ArgumentException($"Wrong year: {year}", nameof(year));
            if (day is < 1 or > 25)
                throw new ArgumentException($"Wrong day: {day}", nameof(day));
            
            var dirInfo = Directory.CreateDirectory($"aoc_inputs/{year}");
            var filePath = Path.Combine(dirInfo.FullName, $"Day{day}_input");
            
            if (!File.Exists(filePath)) {
                Client.Value.DefaultRequestHeaders.Add("Cookie",$"session={_session}");
                var uri = $"https://adventofcode.com/{year}/day/{day}/input";
                var responseMessage = await Client.Value.GetAsync(uri);
                var data = await responseMessage.Content.ReadAsStringAsync();
                try {
                    responseMessage.EnsureSuccessStatusCode();
                    await File.WriteAllTextAsync(filePath, data);
                    return data.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[..^1];
                }
                catch (HttpRequestException ex) {
                    throw new Exception(data);
                }
            }
            
            return await File.ReadAllLinesAsync(filePath);
        }
    }
}