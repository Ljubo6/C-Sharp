using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._Stack_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] line = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> stack = new Stack<int>(line);
            string input = string.Empty;
            while ((input = Console.ReadLine().ToLower()) != "end")
            {
                string[] tokens = input.Split();
                string command = tokens[0];
                if (command == "add")
                {
                    stack.Push(int.Parse(tokens[1]));
                    stack.Push(int.Parse(tokens[2]));
                }
                else
                {
                    if (stack.Count >= int.Parse(tokens[1]))
                    {
                        for (int i = 0; i < int.Parse(tokens[1]); i++)
                        {
                            stack.Pop();
                        }
                    }
                }
            }
            Console.WriteLine($"Sum: {stack.Sum()}");
        }
    }
}
