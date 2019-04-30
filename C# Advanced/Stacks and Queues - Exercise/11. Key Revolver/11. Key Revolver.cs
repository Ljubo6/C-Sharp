using System;
using System.Collections.Generic;
using System.Linq;

namespace _11._Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int bulletPrice = int.Parse(Console.ReadLine());
            int gunBarellSize = int.Parse(Console.ReadLine());
            int[] bullets = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int[] locks = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int intelligenceValue = int.Parse(Console.ReadLine());
            Stack<int> stack = new Stack<int>(bullets);
            Queue<int> queue = new Queue<int>(locks);
            int shautCount = 0;
            int shautedBulled = 0;
            while (stack.Any() && queue.Any())
            {
                int bullet = stack.Peek();
                int lockSmith = queue.Peek();
                if (bullet <= lockSmith)
                {
                    Console.WriteLine("Bang!");
                    queue.Dequeue();
                }
                else
                {
                    Console.WriteLine("Ping!");
                }
                stack.Pop();
                shautCount++;
                shautedBulled++;
                if (shautCount == gunBarellSize && stack.Count > 0)
                {
                    Console.WriteLine("Reloading!");
                    shautCount = 0;
                }
            }
            int bulletCost = bulletPrice * shautedBulled;
            int earned = intelligenceValue - bulletCost;
            if (queue.Count > 0)
            {
                Console.WriteLine($"Couldn't get through. Locks left: {queue.Count}");
            }
            else
            {
                Console.WriteLine($"{bullets.Length - shautedBulled} bullets left. Earned ${earned}");
            }
        }
    }
}
