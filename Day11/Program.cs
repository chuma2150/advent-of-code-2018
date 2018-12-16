using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        const int GridSize = 300;
        const int SquareSize = 3;
        static readonly Dictionary<(int x, int y, int s), int> SquareSizes = new Dictionary<(int x, int y, int s), int>();

        static void Main(string[] args)
        {
            int.TryParse(File.ReadAllText("./inputs/Input.txt"), out var input);
            CalculateSquareSizes(input);
            Part1_PrintCoordinatesOfLargstPowerSquare(input);
            Part2_PrintCoordinatesAndSizeOfLargestPowerSquareAnySize();
            Console.ReadLine();
        }

        private static void Part1_PrintCoordinatesOfLargstPowerSquare(int input)
        {
            var maxPowerLevelSuqareCoordinates = SquareSizes
                .Where(s => s.Key.s == SquareSize)
                .OrderBy(s => s.Value)
                .LastOrDefault()
                .Key;
            Console.WriteLine($"Coordinates of highest power cell: {maxPowerLevelSuqareCoordinates.x},{maxPowerLevelSuqareCoordinates.y}");
        }

        private static void Part2_PrintCoordinatesAndSizeOfLargestPowerSquareAnySize()
        {
            var largestPowerSquare = SquareSizes
                .OrderBy(s => s.Value)
                .LastOrDefault()
                .Key;
            Console.WriteLine($"Coordinates and size of highest power cell: {largestPowerSquare.x},{largestPowerSquare.y},{largestPowerSquare.s}");
        }

        private static void CalculateSquareSizes(int input)
        {
            for (var s = 1; s <= GridSize; s++)
            {
                for (var x = 1; x <= GridSize; x++)
                {
                    if (x + s > GridSize + 1) { break; }

                    for (var y = 1; y <= GridSize; y++)
                    {
                        if (y + s > GridSize + 1) { break; }

                        if (s == 1) { SquareSizes.Add((x, y, s), GetPowerLeveL(x, y, input)); }
                        else
                        {
                            var powerLevelSmallerSquare = SquareSizes[(x, y, s - 1)];
                            for (var sy = y; sy <= y + s - 1; sy++) { powerLevelSmallerSquare += GetPowerLeveL(x + s - 1, sy, input); }
                            for (var sx = x; sx <= x + s - 2; sx++) { powerLevelSmallerSquare += GetPowerLeveL(sx, y + s - 1, input); }
                            SquareSizes.Add((x, y, s), powerLevelSmallerSquare);
                        }
                    }
                }
            }            
        }        

        private static int GetPowerLeveL(int x, int y, int serialNumber)
        {
            var rackId = x + 10;
            var powerLevelStart = rackId * y;
            var powerLevel = powerLevelStart += serialNumber;
            powerLevel *= rackId;
            powerLevel = powerLevel % 1000 / 100;
            return powerLevel - 5;
        }
    }
}
