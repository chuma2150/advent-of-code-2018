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

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"))
                .Select(i =>
                {
                    var splittetInput = i.Split(", ");
                    return new Point(int.Parse(splittetInput[0]), int.Parse(splittetInput[1]));
                });
            Part1_PrintLargestFiniteAreaSize(inputs);
            Console.ReadLine();
        }

        private static void Part1_PrintLargestFiniteAreaSize(IEnumerable<Point> inputs)
        {
            var distancesFromPointToCoordinates = new Dictionary<Point, Dictionary<Point, int>>();
            for (var x = 0; x <= GraphSize; x++)
            {
                for (var y  = 0; y <= GraphSize; y++)
                {
                    var point = new Point(x, y);
                    distancesFromPointToCoordinates.Add(point, new Dictionary<Point, int>());
                    foreach(var coordinate in inputs)
                    {
                        var distance = Math.Abs(point.X - coordinate.X) + Math.Abs(point.Y - coordinate.Y);
                        distancesFromPointToCoordinates[point].Add(coordinate, distance);
                    }
                }
            }

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
    }
}
