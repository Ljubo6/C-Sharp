using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Santa_s_New_List
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            string childName = string.Empty;
            string typeOfToy = string.Empty;
            int amount = 0;
            Dictionary<string, int> childNameAndAmount = new Dictionary<string, int>();
            Dictionary<string, int> toysAndAmount = new Dictionary<string, int>();

            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split("->");
                if (tokens[0] != "Remove")
                {
                    childName = tokens[0];
                    typeOfToy = tokens[1];
                    amount = int.Parse(tokens[2]);

                    if (!childNameAndAmount.ContainsKey(childName))
                    {
                        childNameAndAmount.Add(childName,0);
                    }
                    childNameAndAmount[childName] += amount;

                    if (!toysAndAmount.ContainsKey(typeOfToy))
                    {
                        toysAndAmount.Add(typeOfToy,0);
                    }
                    toysAndAmount[typeOfToy] += amount;
                }
                else
                {
                    childName = tokens[1];
                    childNameAndAmount.Remove(childName);
                }
                
            }
            Console.WriteLine("Children:");
            foreach (var kvp in childNameAndAmount.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
            Console.WriteLine("Presents:");
            foreach (var kvp in toysAndAmount)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }
}
