using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _04._Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            Stack<int> bottles = new Stack<int>(Console.ReadLine().Split().Select(int.Parse).ToArray());
            int wastedSum = 0;
            while (cups.Any() && bottles.Any())
            {
                int cup = cups.Peek();
                while (true)
                {
                    int bottle = bottles.Peek();
                    if (bottle - cup >= 0)
                    {
                        wastedSum += (bottle - cup);
                        cups.Dequeue();
                        bottles.Pop();
                        break;
                    }
                    else
                    {
                        cup = Math.Abs(bottle - cup);
                        bottles.Pop();
                    }
                }
            }
            if (cups.Any())
            {
                Console.WriteLine($"Cups: {string.Join(" ",cups)}");
            }
            if (bottles.Any())
            {
                Console.WriteLine($"Bottles: {string.Join(" ", bottles)}");
            }
            Console.WriteLine($"Wasted litters of water: {wastedSum}");
        }
    }
}
