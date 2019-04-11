using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Pokemon_Evolution
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "wubbalubbadubdub")
            {
                string[] tokens = input.Split(new[] {" -> "},StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                if (tokens.Length > 1)
                {
                    string typeIndex = $"{tokens[1]} <-> {tokens[2]}";
                    if (!dict.ContainsKey(name))
                    {
                        dict.Add(name,new List<string>());
                    }
                    dict[name].Add(typeIndex);
                }
                else
                {
                    if (dict.ContainsKey(name))
                    {
                        Console.WriteLine($"# {name}");
                        foreach (var item in dict[name])
                        {
                            Console.WriteLine(item);
                        }
                    }

                }
            }
            foreach (var kvp in dict)
            {
                Console.WriteLine($"# {kvp.Key}");
                foreach (var item in kvp.Value
                    .OrderByDescending(x => int.Parse(x.Split(new[] {" <-> "},StringSplitOptions.RemoveEmptyEntries).Skip(1).First())))
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
