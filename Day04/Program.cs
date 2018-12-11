using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day04
{
    class Program
    {
        const string GuardEventTag = "Guard #";

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"))
                .Select(i => {
                    var splittetInput = i.Split("] ");
                    return new
                    {
                        Time = DateTime.Parse(splittetInput[0].Substring(1)),
                        Event = splittetInput[1]
                    };
                })
                .OrderBy(i => i.Time);
            Part1_PrintMostAsleepGuardId(inputs);
            //Part2_Print(inputs, points);
            Console.ReadLine();
        }

        private static void Part1_PrintMostAsleepGuardId(IEnumerable<dynamic> inputs)
        {
            var sleepMinutesByGuard = new Dictionary<int, Dictionary<int, int>>();
            var guardId = 0;
            var feltAsleep = DateTime.MinValue;
            foreach(var input in inputs)
            {
                string eventTag = input.Event;
                if (eventTag.Contains(GuardEventTag))
                {
                    var splittedEvent = eventTag.Split(GuardEventTag);
                    int.TryParse(splittedEvent[1].Substring(0, splittedEvent[1].IndexOf(" ")), out guardId);
                    sleepMinutesByGuard.TryAdd(guardId, new Dictionary<int, int>());
                } else if (string.Compare(eventTag, "falls asleep") == 0)
                {
                    feltAsleep = input.Time;
                } else
                {
                    DateTime wokeUp = input.Time;
                    for (var m = feltAsleep.Minute; m < wokeUp.Minute; m++)
                    {
                        if (!sleepMinutesByGuard[guardId].TryAdd(m, 1))
                        {
                            sleepMinutesByGuard[guardId][m]++;
                        }
                    }
                }
            }
            
            var guardIdMaxAsleep = sleepMinutesByGuard
                .OrderBy(g => g.Value.Sum(s => s.Value))
                .LastOrDefault().Key;
            var mostAsleepMinute = sleepMinutesByGuard[guardIdMaxAsleep]
                .OrderBy(s => s.Value)
                .LastOrDefault().Key;

            Console.WriteLine($"Most asleep GuardId ({guardIdMaxAsleep}) multiplied with most asleep minutes ({mostAsleepMinute}): {guardIdMaxAsleep * mostAsleepMinute}");
        }
    }
}
