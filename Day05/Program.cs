using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    class Program
    {
        const int LETTER_DIFFERENCE = 'a' - 'A';

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./inputs/Input.txt");
            Part1_PrintReactedPolymer(input[0]);
            Part2_PrintOptimalReactedPolymer(input[0]);
            Console.ReadLine();
        }

        private static void Part1_PrintReactedPolymer(string input) => Console.WriteLine($"Polymer units: {GetReactedPolymerCount(input)}");

        private static void Part2_PrintOptimalReactedPolymer(string input)
        {
            var minLength = input.Length + 1;
            for (var i = 'A'; i <= 'Z'; i++)
            {
                var optimatedPolymer = input.Replace(i.ToString(), string.Empty).Replace(char.ToLower(i).ToString(), string.Empty);
                var newLength = GetReactedPolymerCount(optimatedPolymer);
                minLength = Math.Min(minLength, newLength);
            }
            Console.WriteLine($"Optimal reacted polymer units: {minLength}");
        }

        private static int GetReactedPolymerCount(string @string)
        {
            var reactedPolymers = new Stack<char>();
            foreach (var @char in @string)
            {
                if (reactedPolymers.TryPeek(out var polymer) && Math.Abs(polymer - @char) == LETTER_DIFFERENCE)
                {
                    reactedPolymers.Pop();
                }
                else
                {
                    reactedPolymers.Push(@char);
                }
            }
            return reactedPolymers.Count();
        }
    }
}
