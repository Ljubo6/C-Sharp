using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Basic_Queue_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] command = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] collection = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Queue<int> queue = new Queue<int>();

            int elementsToEnqueue = command[0];
            int elementToDequeue = command[1];
            int containElement = command[2];

            for (int i = 0; i < elementsToEnqueue; i++)
            {
                queue.Enqueue(collection[i]);
            }
            for (int i = 0; i < elementToDequeue; i++)
            {
                queue.Dequeue();
            }
            if (queue.Contains(containElement))
            {
                Console.WriteLine("true");
            }
            else if (!queue.Contains(containElement) && queue.Count > 0)
            {
                List<int> current = queue.ToList();
                current = current.OrderBy(x => x).ToList();
                Console.WriteLine(current[0]);
            }
            else
            {
                Console.WriteLine("0");
            }
        }
    }
}
