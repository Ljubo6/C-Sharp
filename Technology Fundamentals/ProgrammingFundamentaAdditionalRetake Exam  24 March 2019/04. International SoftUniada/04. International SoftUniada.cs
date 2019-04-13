using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._International_SoftUniada
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, Dictionary<string, int>> contest = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) !="END")
            {
                string[] tokens = input.Split(" -> ");
                string country = tokens[0];
                string name = tokens[1];
                int points = int.Parse(tokens[2]);

                if (!contest.ContainsKey(country))
                {
                    contest.Add(country,new Dictionary<string, int>());
                    contest[country].Add(name,0);
                }
                if (!contest[country].ContainsKey(name))
                {
                    contest[country].Add(name,0);
                }
                contest[country][name] += points;
            }
            foreach (var kvp in contest.OrderByDescending(x => x.Value.Values.Sum()))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value.Values.Sum()}");
                foreach (var KVP in kvp.Value)
                {
                    Console.WriteLine($"-- {KVP.Key} -> {KVP.Value}");
                }
            }
        }
    }
}
