using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadAllText("./Input.txt")
                .Split(" ")
                .Select(int.Parse);

            var index = 0;
            var rootNode = GetNode(inputs, ref index);

            Part1_PrintSumOfMetaData(rootNode);
            Part2_PrintValue(rootNode);
            Console.ReadLine();
        }

        private static void Part1_PrintSumOfMetaData(Node rootNode) => Console.WriteLine(rootNode.Sum);

        private static void Part2_PrintValue(Node rootNode) => Console.WriteLine(rootNode.Value);

        private static Node GetNode(IEnumerable<int> inputs, ref int index)
        {
            var node = new Node();
            var children = inputs.ElementAt(index++);
            var metadata = inputs.ElementAt(index++);
            for (var i = 0; i < children; i++)
            {
                node.Nodes.Add(GetNode(inputs, ref index));
            }

            for (var i = 0; i < metadata; i++)
            {
                node.Metadata.Add(inputs.ElementAt(index++));
            }

            return node;
        }

        private class Node
        {
            internal List<Node> Nodes = new List<Node>();

            internal List<int> Metadata = new List<int>();

            internal int Sum => Metadata.Sum() + Nodes.Sum(n => n.Sum);

            internal int Value
            {
                get
                {
                    if (!Nodes.Any())
                    {
                        return Metadata.Sum();
                    }

                    var value = 0;
                    foreach (var metaData in Metadata)
                    {
                        if (metaData <= Nodes.Count)
                        {
                            value += Nodes[metaData - 1].Value;
                        }
                    }

                    return value;
                }
            }
        }
    }
}
