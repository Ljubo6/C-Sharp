using System;
using System.Collections.Generic;
using System.Linq;

namespace _10._Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> queue = new Queue<string>();
            int greenTime = int.Parse(Console.ReadLine());
            int windowTime = int.Parse(Console.ReadLine());
            string input = string.Empty;
            int carPassed = 0;
           
            while ((input = Console.ReadLine()) != "END")
            {
                if (input != "green")
                {
                    queue.Enqueue(input);
                    continue;
                }
                int secondsLeft = greenTime;
                string car = string.Empty;
                while (secondsLeft > 0 && queue.Any())
                {
                    car = queue.Dequeue();
                    secondsLeft -= car.Length;
                    carPassed++;
                }
                int freeWindowLeft = windowTime + secondsLeft;
                if (freeWindowLeft < 0)
                {

                    int x = Math.Abs(freeWindowLeft);
                    int index = car.Length - x;
                    char symbol = car[index];
                    Console.WriteLine("A crash happened!");
                    Console.WriteLine($"{car} was hit at {symbol}.");
                    return;
                }
            }
            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{carPassed} total cars passed the crossroads.");
        }
    }
}
