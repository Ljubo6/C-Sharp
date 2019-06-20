using System;
using System.Collections.Generic;

namespace _04._Hit_List
{
    class Program
    {
        static void Main(string[] args)
        {
            int targetInfoIndex = int.Parse(Console.ReadLine());
            string input = string.Empty;
            int infoIndex = 0;
            Dictionary<string, SortedDictionary<string, string>> dict = new Dictionary<string, SortedDictionary<string, string>>();
            while ((input = Console.ReadLine()) != "end transmissions")
            {
                string[] tokens = input.Split(new[] { '=', ';', ':' },StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                if (!dict.ContainsKey(name))
                {
                    dict.Add(name,new SortedDictionary<string, string>());
                }
                for (int i = 1; i < tokens.Length - 1; i += 2)
                {
                    string key = tokens[i];
                    string value = tokens[i + 1];
                    if (!dict[name].ContainsKey(key))
                    {
                        dict[name].Add(key,value);
                    }
                    else
                    {
                        dict[name][key] = value;
                    }
                    
                }
            }
            string[] command = Console.ReadLine().Split();
            string nameCommand = command[1];
            Console.WriteLine($"Info on {nameCommand}:");
            foreach (var kvp in dict[nameCommand])
            {
                infoIndex += kvp.Key.Length;
                infoIndex += kvp.Value.Length;
                Console.WriteLine($"---{kvp.Key}: {kvp.Value}");
            }
            Console.WriteLine($"Info index: {infoIndex}");
            if (infoIndex >= targetInfoIndex)
            {
                Console.WriteLine("Proceed");
            }
            else
            {
                Console.WriteLine($"Need {targetInfoIndex - infoIndex} more info.");
            }
        }
    }
}
