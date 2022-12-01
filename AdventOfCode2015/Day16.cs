using AdventOfCodeClient;

namespace AdventOfCode2015;

[AocDay(2015,16)]
public class Day16 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var sueCompStr = @"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1";
        var result1 = 0;
        var result2 = 0;
        var sueCompounds = GetCompounds(sueCompStr, "\n");
        for (var i = 0; i < input.Length; i++)
        {
            var sue = input[i];
            var colonIndex = sue.IndexOf(": ", StringComparison.InvariantCulture);
            var compStr = sue[(colonIndex + 2)..];
            var compounds = GetCompounds(compStr, ", ");
            
            var validResult1 = true;
            foreach (var compound in compounds)
            {
                if (sueCompounds[compound.Key] != compound.Value)
                {
                    validResult1 = false;
                }
            }
            
            if (validResult1)
            {
                result1 = i + 1;
            }
            
            var validResult2 = true;
            foreach (var compound in compounds)
            {
                if (compound.Key is not ("cats" or "trees" or "pomeranians" or "goldfish") 
                         && sueCompounds[compound.Key] != compound.Value)
                {
                    validResult2 = false;
                }
                else if (compound.Key is "cats" or "trees" 
                         && compound.Value <= sueCompounds[compound.Key])
                {
                    validResult2 = false;
                }
                else if (compound.Key is "pomeranians" or "goldfish" 
                         && compound.Value >= sueCompounds[compound.Key])
                {
                    validResult2 = false;
                }                
            }

            if (validResult2)
            {
                result2 = i + 1;
            }
        }

        return new AocDayResult(result1, result2);
    }

    Dictionary<string, int> GetCompounds(string compStr, string spStr)
    {
        var dict = new Dictionary<string, int>();
        
        foreach (var comp in compStr.Split(spStr))
        {
            var sp = comp.Split(": ");
            dict[sp[0]] = int.Parse(sp[1]);
        }

        return dict;
    }
}
