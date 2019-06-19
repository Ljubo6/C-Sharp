using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            int greeenDuration = int.Parse(Console.ReadLine());
            int windowDuration = int.Parse(Console.ReadLine());
            string input = string.Empty;
            Queue<string> cars = new Queue<string>();
            Queue<char> carChar = new Queue<char>();
            int carPassed = 0;
            while ((input = Console.ReadLine()) != "END")
            {
                if (input != "green")
                {
                    cars.Enqueue(input);
                    continue;
                }
                int green = greeenDuration;
                int window = windowDuration;
                while(green > 0 && cars.Any())
                {
                    string tempCar = cars.Dequeue();
                    carChar = new Queue<char>(tempCar.ToCharArray());
                    carPassed++;
                    while (carChar.Any())
                    {
                        carChar.Dequeue();
                        green--;
                        if (green == 0)
                        {
                            while (window > 0 && carChar.Any())
                            {
                                carChar.Dequeue();
                                window--;                                
                            }
                            if (carChar.Any())
                            {
                                char tempChar = carChar.Peek();
                                Console.WriteLine("A crash happened!");
                                Console.WriteLine($"{tempCar} was hit at {tempChar}.");
                                return;
                            }
                        }
                    }
                }
                if (cars.Count == 0)
                {
                    continue;
                }
                else if (cars.Count > 0 && green == 0)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{carPassed} total cars passed the crossroads.");
        }
    }
}
