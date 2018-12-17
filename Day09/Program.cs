using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        private const int MULTIPLICATOR = 100;
        private static int numberOfPlayers;
        private static int pointsOfLastMarble;

        static void Main(string[] args)
        {
            var inputs = File.ReadAllText("./inputs/Input.txt").Split();
            int.TryParse(inputs[0], out numberOfPlayers);
            int.TryParse(inputs[6], out pointsOfLastMarble);

            Part1_PrintWinningScore();
            Part2_PrintWinningScoreMarbleTimes100();
            Console.ReadLine();
        }

        private static void Part1_PrintWinningScore() => Console.WriteLine($"Score of winning elf: {GetScoreBoard().Max()}");

        private static void Part2_PrintWinningScoreMarbleTimes100() => Console.WriteLine(
            $"Score of winning elf marble times {MULTIPLICATOR}: {GetScoreBoard(MULTIPLICATOR).Max()}");

        private static long[] GetScoreBoard(int multiplicator = 1)
        {
            pointsOfLastMarble *= multiplicator;
            var scoreBoard = new long[numberOfPlayers];
            var marbelBoard = new LinkedList<int>();
            marbelBoard.AddFirst(0);
            var currentNode = marbelBoard.First;

            for (var i = 1; i <= pointsOfLastMarble; i++)
            {
                if (i % 23 == 0)
                {
                    for (var j = 0; j < 7; j++)
                    {
                        currentNode = currentNode.Previous ?? marbelBoard.Last;
                    }

                    var valueToRemove = currentNode.Value;
                    var nextNode = currentNode.Next ?? marbelBoard.First;

                    marbelBoard.Remove(currentNode);
                    currentNode = nextNode;
                    scoreBoard[i % numberOfPlayers] += i + valueToRemove;
                }
                else
                {
                    currentNode = currentNode.Next ?? marbelBoard.First;
                    currentNode = marbelBoard.AddAfter(currentNode, i);
                }
            }
            return scoreBoard;
        }
    }
}
