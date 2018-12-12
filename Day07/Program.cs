using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Day07
{
    class Program
    {
        const int TaskListCount = 26;

        static void Main(string[] args)
        {
            var inputs = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Input.txt"))
                .Select(i =>
                {
                    var splittetInput = Regex.Split(i, @"Step (\w) must be finished before step (\w) can begin.");
                    return (char.Parse(splittetInput[1]), char.Parse(splittetInput[2]));
                });
            Part1_PrintTaskOrder(inputs);
            Console.ReadLine();
        }

        private static void Part1_PrintTaskOrder(IEnumerable<(char parent, char child)> inputs)
        {
            var tasks = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var taskOrder = new StringBuilder(TaskListCount, TaskListCount);
            while(taskOrder.Length < TaskListCount)
            {
                for (var i = 0; i < tasks.Length; i++)
                {
                    var nextTask = inputs.Where(t => t.child != tasks[i] && !taskOrder.ToString().Contains(t.parent)).ToList();
                    if (nextTask.Count() == inputs.Where(t => !taskOrder.ToString().Contains(t.parent)).Count())
                    {
                        taskOrder.Append(tasks[i]);
                        tasks = tasks.Remove(i, 1);
                        break;
                    }
                }
            }
            Console.WriteLine($"Taskorder: {taskOrder}");
        }
    }
}
