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

        static void Main(string[] args)
        {
            int.TryParse(File.ReadAllText("./inputs/Input.txt"), out var input);
            Part1_PrintCoordinatesOfFuelCellWithLargestPower(input);
            Console.ReadLine();
        }

        private static void Part1_PrintCoordinatesOfFuelCellWithLargestPower(int input)
        {
            var powerLevels = new Dictionary<(int x, int y), int>();
            for(var x = 1; x <= GridSize; x++)
            {
                for (var y = 1; y <= GridSize; y++)
                {
                    powerLevels.Add((x, y), GetPowerLeveL(x, y, input));
                }
            }

            var maxPowerLevelSuqare = 0;
            var maxPowerLevelSuqareCoordinates = (x: 0, y: 0);
            for (var x = 1; x <= GridSize - SquareSize; x++)
            {
                for (var y = 1; y <= GridSize - SquareSize; y++)
                {
                    var powerLevelSquare = GetPowerLevelForSquare(x, y, powerLevels);
                    if (powerLevelSquare > maxPowerLevelSuqare)
                    {
                        maxPowerLevelSuqare = powerLevelSquare;
                        maxPowerLevelSuqareCoordinates = (x, y);
                    }
                }
            }

            Console.WriteLine($"Coordinates of highest power cell: {maxPowerLevelSuqareCoordinates.x},{maxPowerLevelSuqareCoordinates.y}");
        }

        private static int GetPowerLevelForSquare(int x, int y, Dictionary<(int x, int y), int> powerLevels)
        {
            var powerLevel = 0;
            for (var sx = x; sx <=  x + SquareSize - 1; sx++)
            {
                for (var sy = y; sy <= y + SquareSize - 1; sy++)
                {
                    powerLevel += powerLevels[(sx, sy)];
                }
            }
            return powerLevel;
        }

        private static int GetPowerLeveL(int x, int y, int serialNumber)
        {
            var rackId = x + 10;
            var powerLevelStart = rackId * y;
            var powerLevel = powerLevelStart += serialNumber;
            powerLevel *= rackId;
            powerLevel = (powerLevel % 1000) / 100;
            return powerLevel - 5;
        }
    }
}
