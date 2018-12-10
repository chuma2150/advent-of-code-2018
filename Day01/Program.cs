using System;
using System.Collections.Generic;
using System.IO;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            long? duplicatedFrequency = null;
            var frequency = 0L;
            var frequencies = new List<long>();
            while (duplicatedFrequency == null)
            {
                var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"));
                foreach (var input in inputs)
                {
                    frequencies.Add(frequency);
                    long.TryParse(input, out var frequencyDrift);
                    frequency += frequencyDrift;
                    if (duplicatedFrequency == null && frequencies.Contains(frequency))
                    {
                        duplicatedFrequency = frequency;
                    }
                }
                Console.WriteLine($"End frequency: {frequency}");
            }

            Console.WriteLine($"First dublicated frequency: {duplicatedFrequency}");
            Console.ReadLine();
        }
    }
}
