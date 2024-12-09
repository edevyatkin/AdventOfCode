using AdventOfCodeClient;

namespace AdventOfCode2024;

[AocDay(2024, 9)]
public class Day9 : IAocDay
{
    public AocDayResult Solve(string[] input)
    {
        var result1 = 0L;
        var result2 = 0L;
        
        var diskInfo = input[0].Select(c => c - '0').ToList();
        if (diskInfo.Count % 2 == 1)
            diskInfo.Add(0);
        
        var initialDisk = Allocate(diskInfo);
        
        var disk1 = DefragmentPart1(initialDisk, diskInfo);
        var disk2 = DefragmentPart2(initialDisk, diskInfo);
        
        result1 = CalculateChecksum(disk1);
        result2 = CalculateChecksum(disk2);
        
        return new AocDayResult(result1, result2);
    }

    private int[] Allocate(List<int> diskInfo)
    {
        var disk = new int[diskInfo.Sum()];
        
        var diskIx = 0;
        for (int i = 0; i < diskInfo.Count; i += 2)
        {
            var usedSize = diskInfo[i];
            while (usedSize-- > 0)
            {
                disk[diskIx++] = i / 2;
            }
            diskIx += diskInfo[i + 1];
        }
        
        return disk;
    }

    private int[] DefragmentPart1(int[] initialDisk, List<int> initialDiskInfo)
    {
        var disk = initialDisk.ToArray();
        var diskInfo = initialDiskInfo.ToList();
        
        var diskInfoI = 0;
        var diskInfoJ = diskInfo.Count - 2;
        var i = 0;
        var j = disk.Length - 1;
        while (diskInfoI <= diskInfoJ)
        {
            i += diskInfo[diskInfoI];
            var countFree = diskInfo[diskInfoI+1];
            while (diskInfoI < diskInfoJ && i < disk.Length && countFree > 0)
            {
                if (diskInfo[diskInfoJ] > 0)
                {
                    disk[i++] = diskInfoJ / 2;
                    diskInfo[diskInfoJ]--;
                    disk[j--] = 0;
                    countFree--;
                }
                else
                {
                    j -= diskInfo[diskInfoJ-1];
                    diskInfoJ -= 2;
                }
            }
            diskInfoI += 2;
        }
        
        return disk;
    }

    private int[] DefragmentPart2(int[] initialDisk, List<int> initialDiskInfo)
    {
        var disk = initialDisk.ToArray();
        var diskInfo = initialDiskInfo.ToList();
        
        var freeSpaces = new PriorityQueue<int,int>[10];
        for (var i = 0; i < freeSpaces.Length; i++)
            freeSpaces[i] = new();
        
        var pos = disk.Length;
        for (var i = diskInfo.Count - 2; i >= 0; i -= 2)
        {
            pos -= diskInfo[i + 1];
            if (diskInfo[i + 1] > 0)
                freeSpaces[diskInfo[i + 1]].Enqueue(pos, pos);

            pos -= diskInfo[i];
        }
        
        var filePos = disk.Length;
        for (var i = diskInfo.Count - 2; i >= 0; i -= 2)
        {
            filePos -= diskInfo[i + 1];
            filePos -= diskInfo[i];
            var fileSize = diskInfo[i];
            var leftmostSpacePos = int.MaxValue;
            var leftmostSpace = int.MaxValue;
            var found = false;
            for (var space = fileSize; space < freeSpaces.Length; space++)
            {
                if (fileSize <= space && freeSpaces[space].Count > 0)
                {
                    var spacePos = freeSpaces[space].Peek();
                    if (spacePos > filePos)
                        continue;
                    if (spacePos < leftmostSpacePos)
                    {
                        leftmostSpacePos = spacePos;
                        leftmostSpace = space;
                        found = true;
                    }
                }
            }
            if (!found)
                continue;
            freeSpaces[leftmostSpace].Dequeue();
            Array.Copy(disk, filePos, disk, leftmostSpacePos, fileSize);
            Array.Fill(disk, 0, filePos, fileSize);
            if (leftmostSpace - fileSize > 0)
                freeSpaces[leftmostSpace - fileSize].Enqueue(leftmostSpacePos + fileSize, leftmostSpacePos + fileSize);
        }

        return disk;
    }

    private static long CalculateChecksum(int[] disk)
    {
        var sum = 0L;
        for (var i = 0; i < disk.Length; i++)
            sum += disk[i] * i;
        return sum;
    }
}
