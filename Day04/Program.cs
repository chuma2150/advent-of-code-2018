using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day04
{
    class Program
    {
        const string GuardEventTag = "Guard #";
        static int MaxMinuteGuardId = 0;

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("./inputs/Input.txt")
                .Select(i =>
                {
                    var splittetInput = i.Split("] ");
                    return new
                    {
                        Time = DateTime.Parse(splittetInput[0].Substring(1)),
                        Event = splittetInput[1]
                    };
                })
                .OrderBy(i => i.Time);

            var sleepMinutesByGuard = GetSleepMinuteReport(inputs);
            Part1_PrintMostAsleepGuard(sleepMinutesByGuard);
            Part2_PrintMostAsleepMinuteGuard(sleepMinutesByGuard);
            Console.ReadLine();
        }

        private static Dictionary<int, Dictionary<int, int>> GetSleepMinuteReport(IOrderedEnumerable<dynamic> inputs)
        {
            var sleepMinutesByGuard = new Dictionary<int, Dictionary<int, int>>();
            var guardId = 0;
            var maxSleepMinutes = 0;
            var feltAsleep = DateTime.MinValue;
            foreach (var input in inputs)
            {
                string eventTag = input.Event;
                if (eventTag.Contains(GuardEventTag))
                {
                    var splittedEvent = eventTag.Split(GuardEventTag);
                    int.TryParse(splittedEvent[1].Substring(0, splittedEvent[1].IndexOf(" ")), out guardId);
                    sleepMinutesByGuard.TryAdd(guardId, new Dictionary<int, int>());
                }
                else if (string.Compare(eventTag, "falls asleep") == 0)
                {
                    feltAsleep = input.Time;
                }
                else
                {
                    DateTime wokeUp = input.Time;
                    for (var m = feltAsleep.Minute; m < wokeUp.Minute; m++)
                    {
                        if (!sleepMinutesByGuard[guardId].TryAdd(m, 1))
                        {
                            var newSleepMinutes = sleepMinutesByGuard[guardId][m]++;
                            if (newSleepMinutes > maxSleepMinutes)
                            {
                                maxSleepMinutes = newSleepMinutes;
                                MaxMinuteGuardId = guardId;
                            }
                        }
                    }
                }
            }
            return sleepMinutesByGuard;
        }

        private static void Part1_PrintMostAsleepGuard(Dictionary<int, Dictionary<int, int>> sleepMinutesByGuard)
        {
            var guardIdMaxAsleep = sleepMinutesByGuard
                .OrderBy(g => g.Value.Sum(s => s.Value))
                .LastOrDefault().Key;
            var mostAsleepMinute = GetMostAsleepMinuteFromGuard(guardIdMaxAsleep, sleepMinutesByGuard);

            Console.WriteLine($"Most asleep GuardId ({guardIdMaxAsleep}) multiplied with most asleep minutes ({mostAsleepMinute}): {guardIdMaxAsleep * mostAsleepMinute}");
        }

        private static void Part2_PrintMostAsleepMinuteGuard(Dictionary<int, Dictionary<int, int>> sleepMinutesByGuard)
        {
            var mostAsleepMinute = GetMostAsleepMinuteFromGuard(MaxMinuteGuardId, sleepMinutesByGuard);
            Console.WriteLine($"Most asleep minutes GuardId ({MaxMinuteGuardId}) multiplied with most asleep minutes ({mostAsleepMinute}): {MaxMinuteGuardId * mostAsleepMinute}");
        }

        private static int GetMostAsleepMinuteFromGuard(int guardIdMaxAsleep, Dictionary<int, Dictionary<int, int>> sleepMinutesByGuard) => sleepMinutesByGuard[guardIdMaxAsleep]
            .OrderBy(s => s.Value)
            .LastOrDefault().Key;
    }
}
