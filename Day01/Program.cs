using System;
using System.IO;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"));
            var frequency = 0L;
            foreach(var input in inputs)
            {
                long.TryParse(input, out var frequencyDrift);
                frequency += frequencyDrift;
            }
            Console.WriteLine(frequency);
            Console.ReadLine();
        }
    }
}
