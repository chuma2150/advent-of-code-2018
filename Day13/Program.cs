using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        private static readonly Dictionary<char, Direction> cartMappings = new Dictionary<char, Direction>
        {
            { '<', Direction.Left },
            { '>', Direction.Right },
            { '^', Direction.Up },
            { 'v', Direction.Down }
        };
        private static readonly Dictionary<char, Direction> curveMappings = new Dictionary<char, Direction>
        {
            { '\\', Direction.Left },
            { '/', Direction.Right }
        };
        private static readonly List<InterSection> intersections = new List<InterSection>();
        private static readonly List<Curve> curves = new List<Curve>();
        private static List<Cart> carts = new List<Cart>();

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines("./inputs/Input.txt");
            CreateCartAndTrackSystem(inputs);
            Part1_PrintLocationOfFirstCollition();
            Part2_PrintLocationOfLastCart();
            Console.ReadLine();
        }

        private static void Part1_PrintLocationOfFirstCollition()
        {
            var logCrash = true;
            while (carts.Count() > 1)
            {
                foreach (var cart in carts.OrderBy(c => c.Y).OrderBy(c => c.X).ToList()) {
                    cart.Move();

                    var crashedPositions = carts
                        .GroupBy(c => (X: c.X, Y: c.Y))
                        .ToDictionary(c => (X: c.Key.X, Y: c.Key.Y), c => c.Count())
                        .Where(c => c.Value > 1);
                    if (crashedPositions.Any())
                    {
                        var (X, Y) = crashedPositions.First().Key;
                        if (logCrash)
                        {
                            Console.WriteLine($"First crash occured at: {X},{Y}");
                            logCrash = false;
                        }
                        carts = carts.Where(c => c.X != X || c.Y != Y).ToList();
                    }
                }
            }
        }

        private static void Part2_PrintLocationOfLastCart() => Console.WriteLine($"Last cart at: {carts[0].X},{carts[0].Y}");

        private static void CreateCartAndTrackSystem(string[] inputs)
        {
            for (var y = 0; y < inputs.Length; y++)
            {
                var input = inputs[y];
                for (var x = 0; x < input.Length; x++)
                {
                    if (input[x].Equals('+')) { intersections.Add(new InterSection(x, y)); }
                    else if (curveMappings.TryGetValue(input[x], out var curveDirection)) { curves.Add(new Curve(x, y, curveDirection)); }
                    else if (cartMappings.TryGetValue(input[x], out var direction)) { carts.Add(new Cart(x, y, direction)); }
                }
            }
        }

        internal enum Direction
        {
            None = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            Down = 4
        }

        private class InterSection
        {
            public int X { get; protected set; }

            public int Y { get; protected set; }

            internal InterSection(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private class Curve : InterSection
        {

            public Direction Direction { get; protected set; }

            internal Curve(int x, int y, Direction direction) : base(x, y) => Direction = direction;
        }

        private class Cart : Curve
        {
            private Direction LastIntersectionDirection { get; set; } = Direction.Right;

            internal Cart(int x, int y, Direction direction) : base(x, y, direction) { }

            internal void Move()
            {
                X = Direction == Direction.Right ? X + 1 : Direction == Direction.Left ? X - 1 : X;
                Y = Direction == Direction.Up ? Y - 1 : Direction == Direction.Down ? Y + 1 : Y;
                var occuringIntersections = intersections.Where(i => i.X == X && i.Y == Y);
                var occuringCurves = curves.Where(i => i.X == X && i.Y == Y);
                if (occuringIntersections.Any())
                {
                    SetNewIntersectionDirection();
                    SetDirectionAfterIntersection();
                }
                else if (occuringCurves.Any()) { SetDirectionAfterCurve(occuringCurves.First()); }
            }

            private void SetNewIntersectionDirection()
            {
                switch (LastIntersectionDirection)
                {
                    case Direction.Right:
                        LastIntersectionDirection = Direction.Left;
                        break;
                    case Direction.Left:
                        LastIntersectionDirection = Direction.None;
                        break;
                    case Direction.None:
                        LastIntersectionDirection = Direction.Right;
                        break;
                }
            }

            private void SetDirectionAfterIntersection()
            {
                if (LastIntersectionDirection == Direction.None) { return; }
                switch (Direction)
                {
                    case Direction.Left:
                        Direction = LastIntersectionDirection == Direction.Left ? Direction.Down : Direction.Up;
                        break;
                    case Direction.Right:
                        Direction = LastIntersectionDirection == Direction.Left ? Direction.Up : Direction.Down;
                        break;
                    case Direction.Up:
                        Direction = LastIntersectionDirection;
                        break;
                    case Direction.Down:
                        Direction = LastIntersectionDirection == Direction.Left ? Direction.Right : Direction.Left;
                        break;
                }
            }

            private void SetDirectionAfterCurve(Curve curve)
            {
                switch (Direction)
                {
                    case Direction.Left:
                        Direction = curve.Direction == Direction.Left ? Direction.Up : Direction.Down;
                        break;
                    case Direction.Right:
                        Direction = curve.Direction == Direction.Left ? Direction.Down : Direction.Up;
                        break;
                    case Direction.Up:
                        Direction = curve.Direction == Direction.Left ? Direction.Left : Direction.Right;
                        break;
                    case Direction.Down:
                        Direction = curve.Direction == Direction.Left ? Direction.Right : Direction.Left;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
