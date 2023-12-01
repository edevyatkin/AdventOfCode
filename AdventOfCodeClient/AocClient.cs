using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCodeClient;

public class AocClient {
    private readonly string _session;
    private static readonly Lazy<HttpClient> Client = new();

    public AocClient(string session) {
        if (!Regex.IsMatch(session, "[0-9a-z]{96}"))
            throw new ArgumentException($"Wrong session key: {session}", nameof(session));
        _session = session;
    }
        
    public async Task<string[]> FetchInputAsync(int year, int day) {
        if (year is < 2015 or > 2023)
            throw new ArgumentException($"Wrong year: {year}", nameof(year));
        if (day is < 1 or > 25)
            throw new ArgumentException($"Wrong day: {day}", nameof(day));
            
        var dirInfo = Directory.CreateDirectory($"aoc_inputs/{year}");
        var filePath = Path.Combine(dirInfo.FullName, $"Day{day}_input");

        if (File.Exists(filePath)) 
            return await File.ReadAllLinesAsync(filePath);
        Client.Value.DefaultRequestHeaders.Add("Cookie",$"session={_session}");
        Client.Value.DefaultRequestHeaders.Add("User-Agent",$".NET/6.0 (github.com/edevyatkin/AdventOfCode reddit:u/edevyatkin)");
        var uri = $"https://adventofcode.com/{year}/day/{day}/input";
        var responseMessage = await Client.Value.GetAsync(uri);
        var data = await responseMessage.Content.ReadAsStringAsync();
        if (responseMessage.IsSuccessStatusCode)
        {
            await File.WriteAllTextAsync(filePath, data);
            var splitted = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (splitted[^1] == string.Empty)
                splitted = splitted[..^1];
            return splitted;
        }
        else
        {
            Console.WriteLine(data);
            return Array.Empty<string>();    
        }
    }
}
