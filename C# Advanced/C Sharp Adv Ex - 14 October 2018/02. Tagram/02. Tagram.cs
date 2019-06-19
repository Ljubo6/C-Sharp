using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Tagram
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();
            while ((input = Console.ReadLine()) != "end")
            {
                string[] tokens = input.Split(" -> ");
                if (tokens.Length == 3)
                {
                    string name = tokens[0];
                    string tag = tokens[1];
                    int likes = int.Parse(tokens[2]);

                    if (!dict.ContainsKey(name))
                    {
                        dict.Add(name,new Dictionary<string, int>());
                    }
                    if (!dict[name].ContainsKey(tag))
                    {
                        dict[name].Add(tag,0);
                    }
                    dict[name][tag] = likes;
                }
                else
                {
                    string[] banName = tokens[0].Split();
                    string name = banName[1];

                    if (dict.ContainsKey(name))
                    {
                        dict.Remove(name);
                    }
                }
            }
            foreach (var kvp in dict.OrderByDescending(x => x.Value.Values.Sum()).ThenBy(x => x.Value.Keys.Count()))
            {
                Console.WriteLine($"{kvp.Key}");
                var tempDict = kvp.Value;
                foreach (var KVP in tempDict)
                {
                    Console.WriteLine($"- {KVP.Key}: {KVP.Value}");
                }
            }
        }
    }
}
