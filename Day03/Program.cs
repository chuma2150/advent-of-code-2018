using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"))
                .Select(i => {
                    var splittetInput = Regex.Split(i, @"\#([0-9]+) \@ ([0-9]+)\,([0-9]+)\: ([0-9]+)x([0-9]+)");
                    return new
                    {
                        Id = int.Parse(splittetInput[1]),
                        Left = int.Parse(splittetInput[2]),
                        Top = int.Parse(splittetInput[3]),
                        Width = int.Parse(splittetInput[4]),
                        Height = int.Parse(splittetInput[5])
                    };
                });
            var points = new Dictionary<Point, int>();
            Part1_PrintOverlappingSquareInches(inputs, points);
            Part2_PrintValidClaim(inputs, points);
            Console.ReadLine();
        }

        private static void Part1_PrintOverlappingSquareInches(IEnumerable<dynamic> inputs, Dictionary<Point, int> points)
        {
            foreach(var claim in inputs)
            {
                foreach (var point in GetPoints(claim))
                {
                    if (!points.TryAdd(point, 1))
                    {
                        points[point]++;
                    }
                }
            }
            Console.WriteLine($"Duplicated Inches: {points.Where(p => p.Value > 1).Count()}");
        }

        private static void Part2_PrintValidClaim(IEnumerable<dynamic> inputs, Dictionary<Point, int> points)
        {
            foreach (var claim in inputs)
            {
                if (!HasOverlapps())
                {
                    Console.WriteLine($"Valid ClaimId: {claim.Id}");
                    break;
                }
                bool HasOverlapps()
                {
                    foreach (var point in GetPoints(claim))
                    {
                        if (points[point] > 1)
                        {
                            return true;
                        }
                    }
                    return false;
                };
            }
        }

        private static IEnumerable<Point> GetPoints(dynamic claim)
        {
            for (var y = claim.Top; y < claim.Top + claim.Height; y++)
            {
                for (var x = claim.Left; x < claim.Left + claim.Width; x++)
                {
                    yield return new Point(x, y);
                }
            }
        }
    }
}
