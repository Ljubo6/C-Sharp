using System;
using System.Collections.Generic;

namespace _7._Traffic_Jam
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Queue<string> queue = new Queue<string>();
            int count = 0;
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
                if (command == "green")
                {
                    if (queue.Count < n)
                    {
                        int end = queue.Count;
                        for (int i = 0; i < end; i++)
                        {
                            string car = queue.Dequeue();
                            Console.WriteLine($"{car} passed!");
                            count++;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < n; i++)
                        {
                            string car = queue.Dequeue();
                            Console.WriteLine($"{car} passed!");
                            count++;
                        }
                    }

                }
                else
                {
                    queue.Enqueue(command);
                }
            }
            Console.WriteLine($"{count} cars passed the crossroads.");
        }
    }
}
