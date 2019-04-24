using System;
using System.Collections.Generic;

namespace _5._Supermarket
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = string.Empty;
            Queue<string> queue = new Queue<string>();
            while ((name = Console.ReadLine()) != "End")
            {

                if (name != "Paid")
                {
                    queue.Enqueue(name);
                }
                else
                {
                    int end = queue.Count;
                    for (int i = 0; i < end; i++)
                    {
                        Console.WriteLine(queue.Dequeue());
                    }
                }
            }
            Console.WriteLine($"{queue.Count} people remaining.");
        }
    }
}
