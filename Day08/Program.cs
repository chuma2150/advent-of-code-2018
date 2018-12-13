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
            var rootNode = GetNode(inputs.ToList(), ref index);
            Part1_PrintSumOfMetaData(rootNode);
            Part2_PrintValue(rootNode);
            Console.ReadLine();
        }

        private static void Part1_PrintSumOfMetaData(Node rootNode) => Console.WriteLine(rootNode.Sum());

        private static void Part2_PrintValue(Node rootNode) => Console.WriteLine(rootNode.Value());

        private static Node GetNode(List<int> inputs, ref int index)
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
            internal List<int> Metadata { get; set; } = new List<int>();

            internal List<Node> Nodes { get; set; } = new List<Node>();

            public int Sum() => Metadata.Sum() + Nodes.Sum(n => n.Sum());

            public int Value()
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
                        value += Nodes[metaData - 1].Value();
                    }
                }

                return value;
            }
        }
    }
}
