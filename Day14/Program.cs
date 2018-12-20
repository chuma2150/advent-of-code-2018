using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day14
{
    class Program
    {
        private static readonly List<int> recipeScores = new List<int>() { 3, 7 };

        static void Main(string[] args)
        {
            int.TryParse(File.ReadAllText("./inputs/Input.txt"), out var input);
            GenerateRecipeScores(input + 10, input.ToString());
            Part1_PrintScoresOfTenRecipesAfterNumber(input);
            Part2_PrintNumberOfReceipesLeftOfScore(input.ToString());
            Console.ReadLine();
        }

        private static void Part1_PrintScoresOfTenRecipesAfterNumber(int recipeNumberAfter) => Console
            .WriteLine($"Score of the ten recipes after recipe number {recipeNumberAfter}: {string.Join(string.Empty, recipeScores.Skip(recipeNumberAfter).Take(10))}");
        private static void Part2_PrintNumberOfReceipesLeftOfScore(string scoreSequence) => Console
            .WriteLine($"Number of recipes left of score {scoreSequence}: {string.Join(string.Empty, recipeScores).Split(scoreSequence)[0].Length}");

        private static void GenerateRecipeScores(int minRecipeCount, string scoreSequenceToContain)
        {            
            var posElfOne = 0;
            var posElfTwo = 1;
            var stringContained = false;

            while (recipeScores.Count < minRecipeCount || !stringContained)
            {
                var scoreElfOne = recipeScores[posElfOne];
                var scoreElfTwo = recipeScores[posElfTwo];
                foreach(var digit in (scoreElfOne + scoreElfTwo).ToString())
                {
                    recipeScores.Add(int.Parse(digit.ToString()));
                }

                stringContained = string.Join(string.Empty, recipeScores.Skip(recipeScores.Count - 10)).Contains(scoreSequenceToContain);                

                posElfOne = (posElfOne + scoreElfOne + 1) % recipeScores.Count;
                posElfTwo = (posElfTwo + scoreElfTwo + 1) % recipeScores.Count;
            }
        }
    }
}
