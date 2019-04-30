using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Auto_Repair_and_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] vehicle = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Queue<string> queue = new Queue<string>(vehicle);
            Stack<string> stack = new Stack<string>();
            List<string> list = new List<string>();
            string input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                string[] tokens = input.Split('-');
                string command = tokens[0];
                if (command == "CarInfo")
                {
                    string modelName = tokens[1];
                    if (queue.Contains(modelName))
                    {
                        Console.WriteLine("Still waiting for service.");
                    }
                    else
                    {
                        Console.WriteLine("Served.");
                    }
                }
                else if (command == "Service")
                {
                    if (queue.Count > 0)
                    {
                        string vehicleName = queue.Dequeue();
                        stack.Push(vehicleName);
                        Console.WriteLine($"Vehicle {vehicleName} got served.");
                    }

                }
                else if (command == "History")
                {
                    if (stack.Count > 0)
                    {
                        list = stack.ToList();
                        list.Reverse();
                        Console.WriteLine(string.Join(", ", stack));
                    }


                }
            }
            if (queue.Count > 0)
            {
                Console.WriteLine($"Vehicles for service: {string.Join(", ", queue)}");
            }
            if (stack.Count > 0)
            {
                Console.WriteLine($"Served vehicles: {string.Join(", ", stack)}");
            }

        }
    }
}
