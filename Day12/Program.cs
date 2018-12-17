using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day12
{
    class Program
    {
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
            Part2_PrintNumberOfPlantsAfter5BillionGenerations(initialState, plantPatterns);
            Console.ReadLine();
        }

        private static void Part1_PrintNumberOfPlantsAfter20Generations(string initialState, IEnumerable<(string, char)> plantPatterns) => CalculateGenerartion(
            initialState,
            plantPatterns,
            20);

        private static void Part2_PrintNumberOfPlantsAfter5BillionGenerations(string initialState, IEnumerable<(string, char)> plantPatterns) => CalculateGenerartion(
            initialState,
            plantPatterns,
            50000000000);

        private static void CalculateGenerartion(string initialState, IEnumerable<(string, char)> plantPatterns, long numberOfGenerations)
        {
            var currentGeneration = initialState;
            var previousPlantGrowth = 0L;
            var previousPlantSum = CalculatePlantSum(currentGeneration);
            var plantGrowth = 0L;
            var plantSum = 0L;
            for (var i = 0; i < numberOfGenerations; i++)
            {
                currentGeneration = GetNextGeneration(currentGeneration, plantPatterns);
                plantSum = CalculatePlantSum(currentGeneration);
                plantGrowth = plantSum - previousPlantSum;
                if (previousPlantGrowth == plantGrowth)
                {
                    plantSum += plantGrowth * (numberOfGenerations - i + 2);
                    break;
                }
                previousPlantGrowth = plantGrowth;
                previousPlantSum = plantSum;
            }
            Console.WriteLine($"Number of plants after {numberOfGenerations} Generations: {plantSum}");
        }

        private static string GetNextGeneration(string currentGeneration, IEnumerable<(string pattern, char result)> plantPatterns)
        {
            var newGeneraton = new StringBuilder();
            newGeneraton.Append(currentGeneration.Substring(0, 2));
            for (var i = 2; i < currentGeneration.Length - 2; i++)
            {
                var partToCompare = currentGeneration.Substring(i - 2, 5);
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

        private static long CalculatePlantSum(string generation)
        {
            var sum = 0L;
            for (var i = 0; i < generation.Length; i++)
            {
                if (generation[i].Equals(PLANT_INDICATOR)) { sum += i - offsetX; }
            }
            return sum;
        }
    }
}
