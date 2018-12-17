using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        const int GRID_SIZE = 300;
        const int SQUARE_SIZE = 3;
        static readonly Dictionary<(int x, int y, int s), int> squareSizes = new Dictionary<(int x, int y, int s), int>();

        static void Main(string[] args)
        {
            int.TryParse(File.ReadAllText("./inputs/Input.txt"), out var input);
            CalculateSquareSizes(input);
            Part1_PrintCoordinatesOfLargstPowerSquare();
            Part2_PrintCoordinatesAndSizeOfLargestPowerSquareAnySize();
            Console.ReadLine();
        }

        private static void Part1_PrintCoordinatesOfLargstPowerSquare()
        {
            var maxPowerLevelSuqareCoordinates = squareSizes
                .Where(s => s.Key.s == SQUARE_SIZE)
                .OrderBy(s => s.Value)
                .LastOrDefault()
                .Key;
            Console.WriteLine($"Coordinates of highest power cell: {maxPowerLevelSuqareCoordinates.x},{maxPowerLevelSuqareCoordinates.y}");
        }

        private static void Part2_PrintCoordinatesAndSizeOfLargestPowerSquareAnySize()
        {
            var largestPowerSquare = squareSizes
                .OrderBy(s => s.Value)
                .LastOrDefault()
                .Key;
            Console.WriteLine($"Coordinates and size of highest power cell: {largestPowerSquare.x},{largestPowerSquare.y},{largestPowerSquare.s}");
        }

        private static void CalculateSquareSizes(int input)
        {
            for (var squareSize = 1; squareSize <= GRID_SIZE; squareSize++)
            {
                for (var x = 1; x <= GRID_SIZE; x++)
                {
                    if (x + squareSize > GRID_SIZE + 1) { break; }

                    for (var y = 1; y <= GRID_SIZE; y++)
                    {
                        if (y + squareSize > GRID_SIZE + 1) { break; }

                        if (squareSize == 1) { squareSizes.Add((x, y, squareSize), GetPowerLeveL(x, y, input)); }
                        else
                        {
                            var powerLevelSmallerSquare = squareSizes[(x, y, squareSize - 1)];
                            for (var squareY = y; squareY <= y + squareSize - 1; squareY++) { powerLevelSmallerSquare += GetPowerLeveL(x + squareSize - 1, squareY, input); }
                            for (var squareX = x; squareX <= x + squareSize - 2; squareX++) { powerLevelSmallerSquare += GetPowerLeveL(squareX, y + squareSize - 1, input); }
                            squareSizes.Add((x, y, squareSize), powerLevelSmallerSquare);
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
