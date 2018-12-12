using System;
using System.Collections.Generic;
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
            Part2_PrintTimeNeededForTasks(inputs);
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

        private static void Part2_PrintTimeNeededForTasks(IEnumerable<(char parent, char child)> inputs)
        {
            var base64ToTimeDiff = 4;
            var maxWorkers = 5;
            var tasks = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var tasksWithTime = new Dictionary<char, int>();
            foreach (var task in tasks)
            {
                tasksWithTime.Add(task, task - base64ToTimeDiff);
            }
            var taskQueue = new List<char>();
            var taskCompleted = new List<char>();
            var timeElapsed = 0;
            while (tasksWithTime.Any(t => t.Value > 0))
            {
                for (var i = 0; i < tasks.Length; i++)
                {
                    var nextTask = inputs.Where(t => t.child != tasks[i] && !taskCompleted.Contains(t.parent)).ToList();
                    if (nextTask.Count() == inputs.Where(t => !taskCompleted.Contains(t.parent)).Count())
                    {
                        if (!taskQueue.Contains(tasks[i]) && taskQueue.Count() < maxWorkers)
                        {
                            taskQueue.Add(tasks[i]);
                        }
                    }
                }
                timeElapsed++;
                foreach (var task in taskQueue.ToList())
                {
                    tasksWithTime[task]--;
                    if (tasksWithTime[task] == 0)
                    {
                        tasks = tasks.Remove(tasks.IndexOf(task), 1);
                        taskQueue.Remove(task);
                        taskCompleted.Add(task);
                    }
                }
            }
            Console.WriteLine($"Time needed for tasks: {timeElapsed}");
        }
    }
}
