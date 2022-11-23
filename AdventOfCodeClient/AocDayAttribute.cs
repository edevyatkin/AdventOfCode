using System;

namespace AdventOfCodeClient;

[AttributeUsage(AttributeTargets.Class)]  
public class AocDayAttribute : Attribute
{
    public int Year { get; init; }
    public int Day { get; init; }

    public AocDayAttribute(int year, int day)
    {
        Year = year;
        Day = day;
    }
}
