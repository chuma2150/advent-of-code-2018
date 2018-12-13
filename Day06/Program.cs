using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day06
{
    class Program
    {
        const int GraphSize = 360;
        static int SafeArea = 0;

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("./inputs/Input.txt")
                .Select(i =>
                {
                    var splittetInput = i.Split(", ");
                    return new Point(int.Parse(splittetInput[0]), int.Parse(splittetInput[1]));
                });
            var distancesFromPointToCoordinates = GetCalculatedDistances(inputs);
            Part1_PrintLargestFiniteAreaSize(distancesFromPointToCoordinates);
            Part2_PrintSizeOfSafeArea(distancesFromPointToCoordinates);
            Console.ReadLine();
        }

        private static void Part1_PrintLargestFiniteAreaSize(Dictionary<Point, Dictionary<Point, int>> distancesFromPointToCoordinates)
        {
            var closestCoordinateToPoint = new Dictionary<Point, Point>();
            foreach(var point in distancesFromPointToCoordinates)
            {
                var closestCoordinates = distancesFromPointToCoordinates[point.Key].OrderBy(d => d.Value).ToList();
                if (closestCoordinates[0].Value != closestCoordinates[1].Value)
                {
                    closestCoordinateToPoint.Add(point.Key, closestCoordinates[0].Key);
                }
            }

            var closestFiniteCoordinateToPoint = closestCoordinateToPoint
                .GroupBy(e => e.Value)
                .ToDictionary(v => v.Key, v => v.Select(p => p.Key))
                .Where(e => !e.Value.Any(p => p.X == 0 || p.X == GraphSize || p.Y == 0 || p.Y == GraphSize));

            var finiteCoordinateWithMaxClosestPoints = closestFiniteCoordinateToPoint
                .OrderBy(p => p.Value.Count())
                .Select(p => p.Value.Count())
                .LastOrDefault();
            Console.WriteLine($"Size of largest finite area: {finiteCoordinateWithMaxClosestPoints}");
        }

        private static void Part2_PrintSizeOfSafeArea(Dictionary<Point, Dictionary<Point, int>> distancesFromPointToCoordinates) => Console
            .WriteLine($"Size of safe area: {SafeArea}");

        private static Dictionary<Point, Dictionary<Point, int>> GetCalculatedDistances(IEnumerable<Point> inputs)
        {
            var distancesFromPointToCoordinates = new Dictionary<Point, Dictionary<Point, int>>();
            for (var x = 0; x <= GraphSize; x++)
            {
                for (var y = 0; y <= GraphSize; y++)
                {
                    var totalDistance = 0;
                    var point = new Point(x, y);
                    distancesFromPointToCoordinates.Add(point, new Dictionary<Point, int>());
                    foreach (var coordinate in inputs)
                    {
                        var distance = Math.Abs(point.X - coordinate.X) + Math.Abs(point.Y - coordinate.Y);
                        totalDistance += distance;
                        distancesFromPointToCoordinates[point].Add(coordinate, distance);
                    }
                    if (totalDistance < 10000)
                    {
                        SafeArea++;
                    }
                }
            }
            return distancesFromPointToCoordinates;
        }
    }
}
