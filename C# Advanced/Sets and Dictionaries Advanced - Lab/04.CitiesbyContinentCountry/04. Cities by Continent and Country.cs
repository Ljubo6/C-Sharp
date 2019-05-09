using System;
using System.Collections.Generic;

namespace _04.CitiesbyContinentCountry
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, Dictionary<string, List<string>>> dict = new Dictionary<string, Dictionary<string, List<string>>>();
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine().Split();
                string continent = tokens[0];
                string country = tokens[1];
                string cities = tokens[2];
                if (!dict.ContainsKey(continent))
                {
                    dict.Add(continent,new Dictionary<string, List<string>>());

                }
                if (!dict[continent].ContainsKey(country))
                {
                    dict[continent].Add(country, new List<string>());
                }
                dict[continent][country].Add(cities);
            }
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key}:");
                var countryCity = kvp.Value;
                foreach (var KVP in countryCity)
                {
                    Console.WriteLine($"  {KVP.Key} -> {string.Join(", ",KVP.Value)}");
                }
            }
        }
    }
}
