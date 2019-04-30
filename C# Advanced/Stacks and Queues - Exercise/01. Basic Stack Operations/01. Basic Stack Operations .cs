using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] command = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] collection = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Stack<int> stack = new Stack<int>();

            int elementsToPush = command[0];
            int elementToPop = command[1];
            int containElement = command[2];

            for (int i = 0; i < elementsToPush; i++)
            {
                stack.Push(collection[i]);
            }
            for (int i = 0; i < elementToPop; i++)
            {
                stack.Pop();
            }
            if (stack.Contains(containElement))
            {
                Console.WriteLine("true");
            }
            else if (!stack.Contains(containElement) && stack.Count > 0)
            {
                stack.OrderByDescending(x => x);
                Console.WriteLine(stack.Pop());
            }
            else
            {
                Console.WriteLine("0");
            }
        }
    }
}
