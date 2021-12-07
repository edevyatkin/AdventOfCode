using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public class Day13
    {
        public static void Main(string[] args)
        {
            var data = File.ReadAllLines("Day13_input.txt");
            int t = int.Parse(data[0]);
            var buses = data[1].Split(',');
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < buses.Length; i++)
            {
                if (buses[i] == "x")
                    continue;
                dict[int.Parse(buses[i])] = (buses.Length-1)-i;
            }

            (int BusId, long WaitTime) GetNextBus(long t)
            {
                int id = 0;
                long wait = long.MaxValue;
                foreach (var bus in dict)
                {
                    var busId = bus.Key;
                    if (busId - (t % busId) <= wait)
                    {
                        wait = busId - (t % busId);
                        id = busId;
                    }
                }

                return (id, wait);
            }

            var busData = GetNextBus(t);
            Console.WriteLine(busData.BusId * busData.WaitTime);

            // long index = buses.First().Item2;
            // int count;
            //
            // while (true)
            // {
            //     if (index % 10000000 == 0)
            //         Console.WriteLine(index);
            //     count = 0;
            //     for (int i = 1; i < buses.Count; i++)
            //     {
            //         if ((index + buses[i].i) % buses[i].Item2 == 0)
            //            count++;
            //         else
            //             break;
            //     }
            //
            //     if (count == buses.Count)
            //     {
            //         Console.WriteLine(index);
            //         break;
            //     }
            //
            //     index += buses.First().Item2;
            // }

            // long time = 0;
            // LinkedList<long> busQueue = new LinkedList<long>();
            // //busQueue.AddLast(0);
            // while (true)
            // {
            //     if (time % 10000000 == 0)
            //         Console.WriteLine(time);
            //     var nextBus = GetNextBus(time);
            //     // long startTime = time + nextBus.WaitTime - dict[nextBus.BusId];
            //     time += nextBus.WaitTime;
            //     busQueue.AddLast(time);
            //     if (dict[nextBus.BusId] != buses.Length-1)
            //         continue;
            //     while (busQueue.Last.Value - busQueue.First.Value + 1 > buses.Length)
            //     {
            //         busQueue.RemoveFirst();
            //     }
            //     if (busQueue.Count == dict.Count && busQueue.Last.Value - busQueue.First.Value + 1 == buses.Length)
            //     {
            //         var queueArray = busQueue.ToArray();
            //         int ind = 0;
            //         bool isGood = true;
            //         foreach (var bus in dict)
            //         {
            //             if (queueArray[ind] % bus.Key != 0 || queueArray[ind] != queueArray[0] + bus.Value)
            //             {
            //                 isGood = false;
            //                 break;
            //             }
            //
            //             ind++;
            //         }
            //
            //         if (isGood == true)
            //         {
            //             Console.WriteLine(busQueue.First.Value);
            //             Console.WriteLine(string.Join(' ', busQueue.ToArray()));
            //             break;
            //         }
            //     }
            // }
            var M = dict.Keys.Aggregate(1L, (x, y) => x * y);
            var Mi = dict.Keys.Select(num => M / num).ToList();
            var MiInv = new List<long>();
            var nums = dict.Keys.Select(x => (long)x).ToList();
            var rems = dict.Values.Select(x => (long)x).ToList();
            for (int i = 0; i < Mi.Count; i++) {
                MiInv.Add(Inverse(Mi[i],nums[i]));
            }

            long sum = 0;
            for (int i = 0; i < nums.Count; i++) {
                sum += rems[i] * Mi[i] * MiInv[i];
            }
            // var list = new List<HashSet<int>>();
            // foreach (var kvp in dict) {
            //     var x = kvp.Value;
            //     var hs = new HashSet<int>();
            //     while (x < countTo) {
            //         hs.Add(x);
            //         x += kvp.Key;
            //     }
            //     list.Add(hs);
            // }
            //
            // var intersectHs = list[0];
            // for (int i = 1; i < list.Count; i++) {
            //     intersectHs.IntersectWith(list[i]);
            // }
            // Console.WriteLine(intersectHs.First() - (buses.Length-1));
            Console.WriteLine(sum % M - (buses.Length-1));
        }

        static unsafe void ExtendedEuclid(long a, long b, long *x, long *y, long *d) {
            if (b == 0) {
                *d = a; *x = 1; *y = 0;
                return;
            }

            long x2 = 1; long x1 = 0;
            long y2 = 0; long y1 = 1;
            while (b > 0) {
                var q = a / b; var r = a - q * b;
                *x = x2 - q * x1; *y = y2 - q * y1;
                a = b; b = r;
                x2 = x1; x1 = *x; y2 = y1; y1 = *y;
            }

            *d = a; *x = x2; *y = y2;
        }
        
        static unsafe long Inverse(long a, long n) {
            long d, x, y;
            ExtendedEuclid(a, n, &x, &y, &d);
            return d == 1 ? x : 0;
        }
    }
}