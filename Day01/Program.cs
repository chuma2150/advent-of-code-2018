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
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"));
            Part1(inputs);
            Part2(inputs);
            Console.ReadLine();
        }

        private static void Part1(string[] inputs) => Console.WriteLine($"Frequency: {inputs.Sum(i => long.Parse(i))}");

        private static void Part2(string[] inputs)
        {
            long? duplicatedFrequency = null;
            var frequency = 0L;
            var frequencies = new List<long>();
            while (duplicatedFrequency == null)
            {
                foreach (var input in inputs)
                {
                    frequencies.Add(frequency);
                    long.TryParse(input, out var frequencyDrift);
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
