using System;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var countTwo = 0;
            var countThree = 0;

            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"));
            foreach(var input in inputs)
            {
                var occurrences = input
                    .Where(char.IsLetter)
                    .GroupBy(c => c)
                    .Select(c => new { Letter = c.Key, Count = c.Count() });

                if (occurrences.Any(l => l.Count == 2))
                {
                    countTwo++;                    
                }
                if (occurrences.Any(l => l.Count == 3))
                {
                    countThree++;
                }
            }

            Console.WriteLine($"Matching two ({countTwo}) multiplied with matching three ({countThree}): {countTwo * countThree}");
            Console.ReadLine();
        }
    }
}
