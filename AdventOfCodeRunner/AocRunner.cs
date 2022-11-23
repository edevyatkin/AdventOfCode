using System.Reflection;
using AdventOfCodeClient;
using CommandLine;

namespace AdventOfCodeRunner;

public class AocRunner
{
    class AocOptions
    {
        [Option(Required = true, HelpText = "Year of Advent of Code")]
        public int Year { get; set; }
        [Option(Required = true, HelpText = "Day of Advent of Code")]
        public int Day { get; set; }
    }
    public static async Task Main(string[] args)
    {
        await Parser.Default.ParseArguments<AocOptions>(args).WithParsedAsync(async o =>
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(location!).Where(name => name.EndsWith(".dll")).ToList();
            var allTypes = files
                    .Select(Assembly.LoadFrom)
                    .SelectMany(t => t.GetExportedTypes())
                    .ToList();
            foreach(var type in allTypes)
            {
                var attr = type.GetCustomAttribute<AocDayAttribute>();
                if (type.GetInterface(nameof(IAocDay)) is null || attr?.Year != o.Year || attr.Day != o.Day) 
                    continue;
                var dayClass = Activator.CreateInstance(type);
                var solveMethod = type.GetMethod(nameof(IAocDay.Solve));
                var result = solveMethod!.Invoke(dayClass, new object[] { o.Year, o.Day }) as Task<AocDayResult>;
                var res = await result!;
                Console.WriteLine($"Part 1: {res.Part1}");
                Console.WriteLine($"Part 2: {res.Part2}");
                break;
            }            
        });
    }
}
