using System;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021 {
    public class Day3 {
        public static async Task Main(string[] args) {
            var input = await AocHelper.FetchInputAsync(2021, 3);
            
            // PART 1
            var length = input[0].Length;
            var counts = new int[length];
            foreach (var s in input) {
                for (var index = 0; index < s.Length; index++) {
                    counts[index] += s[index] - '0';
                }
            }
            int gammaRate = 0;
            for (int i = length-1; i >= 0; i--) {
                gammaRate += ((counts[i] > input.Length / 2) ? 1 : 0) * (int)Math.Pow(2,length-1-i);
            }
            int epsilonRate = ((1 << length) - 1) ^ gammaRate;
            int powerConsumption = gammaRate * epsilonRate;
            Console.WriteLine(powerConsumption);
            
            
            // PART 2
            string[] orating = new string[input.Length];
            Array.Copy(input,orating, input.Length);
            int bit = 0;
            while (orating.Length > 1) {
                var oratingOnes = orating.Where(x => x[bit] == '1').ToArray();
                var oratingZeroes = orating.Where(x => x[bit] == '0').ToArray();
                orating = (oratingOnes.Length >= oratingZeroes.Length) ? oratingOnes : oratingZeroes;
                bit++;
            }
            int oxygenGeneratorRating = Convert.ToInt32(orating[0], 2);
            
            string[] crating = new string[input.Length];
            Array.Copy(input,crating, input.Length);
            bit = 0;
            while (crating.Length > 1) {
                var cratingOnes = crating.Where(x => x[bit] == '1').ToArray();
                var cratingZeroes = crating.Where(x => x[bit] == '0').ToArray();
                crating = (cratingZeroes.Length <= cratingOnes.Length) ? cratingZeroes : cratingOnes;
                bit++;
            }
            int c02ScrubberRating = Convert.ToInt32(crating[0], 2);
            
            int lifeSupportRating = oxygenGeneratorRating * c02ScrubberRating;
            Console.WriteLine(lifeSupportRating);
        }
    }
}