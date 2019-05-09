using System;
using System.Collections.Generic;
using System.Linq;

namespace _07._The_V_Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, List<string>> vloggerWithFolowers = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> vloggerWithFolowing = new Dictionary<string, List<string>>();
            while ((input = Console.ReadLine()) != "Statistics")
            {
                string[] tokens = input.Split();
                string vloggerOne = tokens[0];
                string command = tokens[1];
                if (command == "joined")
                {
                    if (!vloggerWithFolowers.ContainsKey(vloggerOne) && !vloggerWithFolowing.ContainsKey(vloggerOne))
                    {
                        vloggerWithFolowers.Add(vloggerOne,new List<string>());
                        vloggerWithFolowing.Add(vloggerOne, new List<string>());
                    }
                }
                else if (command == "followed")
                {
                    string vloggerTwo = tokens[2];
                    if (vloggerWithFolowers.ContainsKey(vloggerOne) 
                        && vloggerWithFolowers.ContainsKey(vloggerTwo) 
                        && vloggerOne != vloggerTwo)
                    {
                        if (!vloggerWithFolowers[vloggerTwo].Contains(vloggerOne))
                        {
                            vloggerWithFolowers[vloggerTwo].Add(vloggerOne);
                        }
                    }
                    if (vloggerWithFolowing.ContainsKey(vloggerOne)
                        && vloggerWithFolowing.ContainsKey(vloggerTwo)
                        && vloggerOne != vloggerTwo)
                    {
                        if (!vloggerWithFolowing[vloggerOne].Contains(vloggerTwo))
                        {
                            vloggerWithFolowing[vloggerOne].Add(vloggerTwo);
                        }
                    }
                }
            }
            Dictionary<string, List<List<string>>> resultDict = new Dictionary<string, List<List<string>>>();
            foreach (var kvp in vloggerWithFolowers)
            {
                resultDict.Add(kvp.Key, new List<List<string>>());
                resultDict[kvp.Key].Add(kvp.Value);
                
            }
            foreach (var kvp in vloggerWithFolowing)
            {
                resultDict[kvp.Key].Add(kvp.Value);

            }
            Console.WriteLine($"The V-Logger has a total of {vloggerWithFolowers.Keys.Count} vloggers in its logs.");
            var dict = resultDict
                .OrderByDescending(x => x.Value[0].Count)
                .ThenBy(x => x.Value[1].Count)
                .ToDictionary(x => x.Key,x => x.Value);
            int count = 0;
            foreach (var kvp in dict)
            {
                count++;
                if (count == 1)
                {
                    Console.WriteLine($"{count}. {kvp.Key} : {kvp.Value[0].Count} followers, {kvp.Value[1].Count} following");
                    foreach (var follower in kvp.Value[0].OrderBy(x => x))
                    {
                        Console.WriteLine($"*  {follower}");
                    }
                }
                else
                {
                    Console.WriteLine($"{count}. {kvp.Key} : {kvp.Value[0].Count} followers, {kvp.Value[1].Count} following");
                }
                
            }
        }
    }
}
