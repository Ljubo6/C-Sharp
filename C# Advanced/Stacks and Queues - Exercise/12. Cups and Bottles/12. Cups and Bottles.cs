using System;
using System.Collections.Generic;
using System.Linq;

namespace _12._Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] cups = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] bottles = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            Queue<int> queueCups = new Queue<int>(cups);
            Stack<int> stackBottles = new Stack<int>(bottles);
            int wastedSum = 0;
            while (queueCups.Any() && stackBottles.Any())
            {
                int cup = queueCups.Peek();
                int bottle = stackBottles.Peek();
                int diff = bottle - cup;
                if (diff >= 0)
                {
                    stackBottles.Pop();
                    queueCups.Dequeue();
                    wastedSum += diff;
                }
                else
                {
                    while (cup > 0)
                    {
                        
                        cup -= stackBottles.Pop();
                        bottle = stackBottles.Peek();
                        diff = bottle - cup;
                        if (diff >= 0)
                        {
                            wastedSum += diff;
                            cup -= stackBottles.Pop();
                        }
                    }
                    queueCups.Dequeue();
                }
            }
            if (queueCups.Count == 0 && stackBottles.Count > 0)
            {
                Console.WriteLine($"Bottles: {string.Join(" ",stackBottles)}");
                Console.WriteLine($"Wasted litters of water: {wastedSum}");
            }
            else if (queueCups.Count > 0 && stackBottles.Count == 0)
            {
                Console.WriteLine($"Cups: {string.Join(" ", queueCups)}");
                Console.WriteLine($"Wasted litters of water: {wastedSum}");
            }
        }
    }
}
