using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day12
{
    class Program
    {
        const int NUMBER_OF_GENERATIONS = 20;
        const char PLANT_INDICATOR = '#';
        static int offsetX = 3;

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("./inputs/Input.txt");
            var initialState = $"...{inputs[0].Substring(inputs[0].IndexOf(PLANT_INDICATOR))}...";
            var plantPatterns = inputs
                .Skip(2)
                .Select(i =>
                {
                    var splittedInput = i.Split();
                    return (splittedInput.FirstOrDefault(), char.Parse(splittedInput.LastOrDefault()));
                });
            Part1_PrintNumberOfPlantsAfter20Generations(initialState, plantPatterns);
            Console.ReadLine();
        }

        private static void Part1_PrintNumberOfPlantsAfter20Generations(string initialState, IEnumerable<(string, char)> plantPatterns)
        {
            var currentGeneration = initialState;
            for (var i = 1; i <= NUMBER_OF_GENERATIONS; i++)
            {
                currentGeneration = GetNextGeneration(currentGeneration, plantPatterns);
            }

            var sumOfPlantsPerIndix = CalculatePlantSum(currentGeneration);
            Console.WriteLine($"Number of plants after {NUMBER_OF_GENERATIONS} Generations: {sumOfPlantsPerIndix}");
        }

        private static string GetNextGeneration(string currentGeneration, IEnumerable<(string pattern, char result)> plantPatterns)
        {
            var newGeneraton = new StringBuilder();
            newGeneraton.Append(currentGeneration.Substring(0, 2));
            for (var i = 2; i < currentGeneration.Length - 2; i++)
            {
                var partToCompare = currentGeneration.Substring(i -2, 5);
                if (plantPatterns.Any(p => p.pattern.Equals(partToCompare)))
                {
                    var pattern = plantPatterns.Where(p => p.pattern.Equals(partToCompare)).First();
                    newGeneraton.Append(pattern.result);
                }
                else { newGeneraton.Append('.'); }
            }

            var returnGeneration = newGeneraton.ToString().TrimEnd('.');
            var indexOffirstPlant = returnGeneration.IndexOf(PLANT_INDICATOR);
            if (indexOffirstPlant < 3)
            {
                returnGeneration = $"...{returnGeneration}";
                offsetX += 3;
            }
            return $"{returnGeneration}...";
        }

        private static int CalculatePlantSum(string generation)
        {
            var sum = 0;
            for(var i = 0; i < generation.Length; i++)
            {
                if (generation[i].Equals(PLANT_INDICATOR)) { sum += i - offsetX; }
            }
            return sum;
        }
    }
}
