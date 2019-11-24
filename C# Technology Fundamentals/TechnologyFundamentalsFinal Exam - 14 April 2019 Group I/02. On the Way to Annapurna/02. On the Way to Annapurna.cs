using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._On_the_Way_to_Annapurna
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, List<string>> storeAndItems = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split(new[] { "->","," },StringSplitOptions.RemoveEmptyEntries);
                if (tokens[0] == "Add" && tokens.Length == 3)
                {
                    string store = tokens[1];
                    string item = tokens[2];
                    if (!storeAndItems.ContainsKey(store))
                    {
                        storeAndItems.Add(store,new List<string>());
                    }
                    storeAndItems[store].Add(item);
                }
                else if (tokens[0] == "Add" && tokens.Length > 3)
                {
                    string store = tokens[1];
                    if (!storeAndItems.ContainsKey(store))
                    {
                        storeAndItems.Add(store,new List<string>());
                    }
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        storeAndItems[store].Add(tokens[i]);
                    }
                    
                }
                else if (tokens[0] == "Remove" && tokens.Length == 2)
                {
                    string store = tokens[1];
                    storeAndItems.Remove(store);
                }
            }
            Console.WriteLine("Stores list:");
            foreach (var kvp in storeAndItems
                .OrderByDescending(x => x.Value.Count())
                .ThenByDescending(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key}");
                var list = kvp.Value;
                foreach (var item in kvp.Value)
                {
                    Console.WriteLine($"<<{item}>>");
                }
            }
        }
    }
}
