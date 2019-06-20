using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int bulledPrice = int.Parse(Console.ReadLine());
            int gunBarell = int.Parse(Console.ReadLine());
            int[] inputBullets = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int[] inputLocks = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            Stack<int> bullets = new Stack<int>(inputBullets);
            Queue<int> locks = new Queue<int>(inputLocks);

            int intelligence = int.Parse(Console.ReadLine());

            int counter = 0;
            while (true)
            {
                int tempBullet = bullets.Peek();
                int tempLock = locks.Peek();

                counter++;
                bullets.Pop();
                if (tempBullet <= tempLock)
                {
                    Console.WriteLine("Bang!");
                    locks.Dequeue();
                }
                else
                {
                    Console.WriteLine($"Ping!");
                }
                if (counter == gunBarell && bullets.Any())
                {
                    counter = 0;
                    Console.WriteLine($"Reloading!");
                }

                if (locks.Count == 0)
                {
                    int moneyEarned = intelligence - (inputBullets.Length - bullets.Count) * bulledPrice;
                    Console.WriteLine($"{bullets.Count} bullets left. Earned ${moneyEarned}");
                    break;
                }
                else if (bullets.Count == 0)
                {
                    Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
                    break;
                }

            }
        }
    }
}
