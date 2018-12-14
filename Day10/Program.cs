using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("./inputs/Input.txt")
                .Select(i =>
                    {
                        var splittedInput = Regex.Split(i, @"position=<\s?(-?\d+), \s?(-?\d+)> velocity=<\s?(-?\d+), \s?(-?\d+)>");
                        return new VelocityPoint(int.Parse(splittedInput[1]), int.Parse(splittedInput[2]), int.Parse(splittedInput[3]), int.Parse(splittedInput[4]));
                    });

            var seconds = 0;
            Part1_PrintMessage(inputs.ToList(), ref seconds);
            Part2_PrintSecondsToGetMessage(seconds);
            Console.ReadLine();
        }

        private static void Part1_PrintMessage(List<VelocityPoint> points, ref int seconds)
        {
            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);
            var newMinX = minX;
            var newMinY = minY;
            var newMaxX = maxX;
            var newMaxY = maxY;
            List<VelocityPoint> previousPoints = null;

            while (newMaxX <= maxX && newMaxY <= maxY)
            {
                seconds++;
                previousPoints = points.Select(p => (VelocityPoint)p.Clone()).ToList();
                minX = previousPoints.Min(p => p.X);
                minY = previousPoints.Min(p => p.Y);
                maxX = previousPoints.Max(p => p.X);
                maxY = previousPoints.Max(p => p.Y);

                foreach (var point in points)
                {
                    point.X += point.VelocityX;
                    point.Y += point.VelocityY;
                }

                newMinX = points.Min(p => p.X);
                newMinY = points.Min(p => p.Y);
                newMaxX = points.Max(p => p.X);
                newMaxY = points.Max(p => p.Y);
            }
            for (var y = minY; y <= maxY; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    Console.Write(previousPoints.Any(p => p.Y == y && p.X == x) ? '#' : '.');
                }
                Console.WriteLine();
            }
            seconds--;
        }

        private static void Part2_PrintSecondsToGetMessage(int seconds) => Console.WriteLine($"Seconds passed to get message: {seconds}");

        private class VelocityPoint : ICloneable
        {
            internal int X { get; set; }

            internal int Y { get; set; }

            internal int VelocityX { get; private set; }

            internal int VelocityY { get; private set; }

            internal VelocityPoint(int X, int Y, int VelocityX, int VelocityY)
            {
                this.X = X;
                this.Y = Y;
                this.VelocityX = VelocityX;
                this.VelocityY = VelocityY;
            }

            public object Clone() => new VelocityPoint(X, Y, VelocityX, VelocityY);
        }
    }
}
