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
            Console.WriteLine($"Running day {o.Day}, {o.Year}...");
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var name = $"AdventOfCode{o.Year}.dll";
            var assemblyFullPath = Path.Combine(path!, name);
            if (!File.Exists(assemblyFullPath))
            {
                Console.WriteLine($"Couldn't found AdventOfCode{o.Year} project");
                return;
            }
            var allTypes = Assembly.LoadFrom(assemblyFullPath).GetExportedTypes();
            foreach(var type in allTypes)
            {
                var attr = type.GetCustomAttribute<AocDayAttribute>();
                if (type.GetInterface(nameof(IAocDay)) is null || attr?.Year != o.Year || attr.Day != o.Day) 
                    continue;
                var dayClass = Activator.CreateInstance(type);
                var solveMethod = type.GetMethod(nameof(IAocDay.Solve));
                var input = await AocHelper.FetchInputAsync(o.Year, o.Day);
                var result = solveMethod!.Invoke(dayClass, [input]) as AocDayResult;
                Console.WriteLine($"Part 1:\n{result!.Part1}");
                if (o is { Year: < 2025, Day: < 25 } or { Year: >= 2025, Day: < 12 })
                {
                    Console.WriteLine();
                    Console.WriteLine($"Part 2:\n{result!.Part2}");
                }
                break;
            }            
        });
    }
}
