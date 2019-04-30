using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Fast_Food
{
    class Program
    {
        static void Main(string[] args)
        {
            int quantity = int.Parse(Console.ReadLine());
            int[] input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            Queue<int> queue = new Queue<int>(input);
            int maxOrder = queue.Max();
            Console.WriteLine(maxOrder);
            int end = queue.Count;
            for (int i = 0; i < end; i++)
            {
                int order = queue.Peek();
                if (quantity >= order)
                {
                    quantity -= order;
                    queue.Dequeue();
                }
                else
                {
                    Console.WriteLine($"Orders left: {string.Join(" ",queue)}");
                    return;
                }
            }
            Console.WriteLine("Orders complete");
        }
    }
}
