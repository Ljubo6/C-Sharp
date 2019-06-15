using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Travel_Map
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, Dictionary<string, int>> travelMap = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split(" > ",StringSplitOptions.RemoveEmptyEntries);
                string country = tokens[0];
                string town = tokens[1];
                int travelCost = int.Parse(tokens[2]);
                if (!travelMap.ContainsKey(country))
                {
                    travelMap.Add(country, new Dictionary<string, int>()); ;
                }
                if (!travelMap[country].ContainsKey(town))
                {
                    travelMap[country].Add(town,travelCost);
                }
                else
                {
                    if (travelMap[country][town] > travelCost)
                    {
                        travelMap[country][town] = travelCost;
                    }
                }
            }
            foreach (var kvp in travelMap.OrderBy(x => x.Key))
            {
                Console.Write($"{kvp.Key} ->");
                var townsAndCosts = kvp.Value;
                foreach (var KVP in townsAndCosts.OrderBy(x => x.Value))
                {
                    Console.Write($" {KVP.Key} -> {KVP.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}
