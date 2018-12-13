using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt")).Select(long.Parse);
            Part1_PrintEndFrequency(inputs);
            Part2_PrintFirstRepeatedFrequency(inputs);
            Console.ReadLine();
        }

        private static void Part1_PrintEndFrequency(IEnumerable<long> inputs) => Console.WriteLine($"Frequency: {inputs.Sum()}");

        private static void Part2_PrintFirstRepeatedFrequency(IEnumerable<long> inputs)
        {
            long? duplicatedFrequency = null;
            var frequency = 0L;
            var frequencies = new List<long>();
            while (duplicatedFrequency == null)
            {
                foreach (var frequencyDrift in inputs)
                {
                    frequencies.Add(frequency);
                    frequency += frequencyDrift;
                    if (duplicatedFrequency == null && frequencies.Contains(frequency))
                    {
                        duplicatedFrequency = frequency;
                        break;
                    }
                }
            }

            Console.WriteLine($"First dublicated frequency: {duplicatedFrequency}");
        }
    }
}
