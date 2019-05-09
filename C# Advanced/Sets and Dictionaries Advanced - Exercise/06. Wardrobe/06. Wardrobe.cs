using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string, int>>();

            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine().Split(" -> ");
                string color = tokens[0];
                List<string> clothes = tokens[1].Split(",").ToList();
                if (!dict.ContainsKey(color))
                {
                    dict.Add(color,new Dictionary<string, int>());
                }
                for (int j = 0; j < clothes.Count; j++)
                {
                    if (!dict[color].ContainsKey(clothes[j]))
                    {
                        dict[color].Add(clothes[j],0);
                    }
                    dict[color][clothes[j]]++;
                }
            }

            string[] find = Console.ReadLine().Split();

            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} clothes:");
                var tempDict = kvp.Value;
                foreach (var KVP in tempDict)
                {
                    if (find[0] == kvp.Key && find[1] == KVP.Key)
                    {
                        Console.WriteLine($"* {KVP.Key} - {KVP.Value} (found!)");
                    }
                    else
                    {
                        Console.WriteLine($"* {KVP.Key} - {KVP.Value}");
                    }
                    

                }
            }
        }
    }
}
