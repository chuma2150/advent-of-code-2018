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
                    var splittetInput = Regex.Split(i, @"\#[^\s]+ \@ ([0-9]+)\,([0-9]+)\: ([0-9]+)x([0-9]+)");
                    return new
                    {
                        Left = int.Parse(splittetInput[1]),
                        Top = int.Parse(splittetInput[2]),
                        Width = int.Parse(splittetInput[3]),
                        Height = int.Parse(splittetInput[4])
                    };
                });
            Part1_PrintOverlappingSquareInches(inputs);
            Console.ReadLine();
        }

        private static void Part1_PrintOverlappingSquareInches(IEnumerable<dynamic> inputs)
        {
            var points = new Dictionary<Point, int>();
            foreach(var claim in inputs)
            {
                for (var y = claim.Top; y < claim.Top + claim.Height; y++)
                {
                    for (var x = claim.Left; x < claim.Left + claim.Width; x++)
                    {
                        var point = new Point(x, y);
                        if (!points.TryAdd(point, 1))
                        {
                            points[point]++;
                        }
                    }
                }
            }

            Console.WriteLine($"Duplicated Inches: {points.Where(p => p.Value > 1).Count()}");
        }
    }
}
