using System;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"));
            Part1_PrintIdsCheckSum(inputs);
            Part2_PrintMergedClosestIds(inputs);
            Console.ReadLine();
        }

        private static void Part1_PrintIdsCheckSum(string[] inputs)
        {
            var countTwo = 0;
            var countThree = 0;

            foreach (var input in inputs)
            {
                var occurrences = input
                    .GroupBy(c => c)
                    .Select(c => c.Count());

                if (occurrences.Any(c => c == 2))
                {
                    countTwo++;
                }

                if (occurrences.Any(c => c == 3))
                {
                    countThree++;
                }
            }

            Console.WriteLine($"Matching two ({countTwo}) multiplied with matching three ({countThree}): {countTwo * countThree}");
        }

        private static void Part2_PrintMergedClosestIds(string[] inputs)
        {
            foreach(var input in inputs)
            {
                var differentStrings = inputs.Where(i => i.Zip(input, (c1, c2) => c1 != c2).Count(c => c) == 1);
                if (differentStrings.Count() > 0)
                {
                    Console.WriteLine(input.Replace(input.Except(differentStrings.FirstOrDefault()).FirstOrDefault().ToString(), string.Empty));
                    break;
                }
            }
        }
    }
}
