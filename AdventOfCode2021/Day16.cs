using System;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCodeClient;

namespace AdventOfCode2021;

[AocDay(2021,16)]
public class Day16 : IAocDay {
    public async Task<AocDayResult> Solve(int year, int day) {
        var input = await AocHelper.FetchInputAsync(year, day);

        var message = input[0].Select(c => 
            Convert.ToString(
                Convert.ToInt32(c.ToString(), 16), 2
                ).PadLeft(4, '0')
            ).Aggregate(string.Empty, (s1, s2) => s1 + s2);

        int i = 0;
        long result1 = CalculateSumVersion(message, ref i);

        int j = 0;
        long result2 = Calculate(message, ref j);

        return new AocDayResult(result1, result2);
    }

    private static long CalculateSumVersion(string message, ref int i) {
        var version = Convert.ToInt32(message[i..(i + 3)],2);
        i += 3;
        var type = Convert.ToInt32(message[i..(i + 3)],2);
        i += 3;
        long sumVersion = version;
        if (type == 4) {
            while (message[i] == '1')
                i += 5;
            i += 5;
        }
        else {
            var lenTypeId = message[i];
            i++;
            if (lenTypeId == '0') {
                var lenInBits = Convert.ToInt32(message[i..(i + 15)], 2);
                i += 15;
                int stopBit = i + lenInBits;
                while (i < stopBit) {
                    sumVersion += CalculateSumVersion(message, ref i);
                }
            }
            else {
                var numOfSubPackets = Convert.ToInt32(message[i..(i + 11)], 2);
                i += 11;
                while (numOfSubPackets > 0) {
                    sumVersion += CalculateSumVersion(message, ref i);
                    numOfSubPackets--;
                }
            }
        }

        return sumVersion;
    }
    
    private static long Calculate(string message, ref int i) {
        var version = Convert.ToInt32(message[i..(i + 3)],2);
        i += 3;
        var type = Convert.ToInt32(message[i..(i + 3)],2);
        i += 3;
        long value = -1;
        if (type == 4) {
            value = 0;
            while (true) {
                value <<= 4;
                value += Convert.ToInt32(message[(i + 1).. (i + 5)], 2);
                int pi = i;
                i += 5;
                if (message[pi] == '0') break;
            }
        }
        else {
            var lenTypeId = message[i];
            i++;
            if (lenTypeId == '0') {
                var lenInBits = Convert.ToInt32(message[i..(i + 15)], 2);
                i += 15;
                int stopBit = i + lenInBits;
                while (i < stopBit) {
                    CalculateValue(message, ref i, type, ref value);
                }
            }
            else {
                var numOfSubPackets = Convert.ToInt32(message[i..(i + 11)], 2);
                i += 11;
                while (numOfSubPackets > 0) {
                    CalculateValue(message, ref i, type, ref value);
                    if (type < 5)
                        numOfSubPackets--;
                    else
                        numOfSubPackets -= 2;
                }
            }
        }

        return value;
    }

    private static void CalculateValue(string message, ref int i, int type, ref long value) {
        switch (type) {
            case 0:
                if (value == -1)
                    value = 0;
                value += Calculate(message, ref i);
                break;
            case 1: 
                if (value == -1)
                    value = 1;
                value *= Calculate(message, ref i);
                break;
            case 2:
                if (value == -1)
                    value = int.MaxValue;
                value = Math.Min(value, Calculate(message, ref i));
                break;
            case 3:
                if (value == -1)
                    value = int.MinValue;
                value = Math.Max(value, Calculate(message, ref i));
                break;
            case 5:
                value = Calculate(message, ref i) > Calculate(message, ref i) ? 1 : 0;
                break;
            case 6:
                value = Calculate(message, ref i) < Calculate(message, ref i) ? 1 : 0;
                break;
            case 7:
                value = Calculate(message, ref i) == Calculate(message, ref i) ? 1 : 0;
                break;
        }
    }
}