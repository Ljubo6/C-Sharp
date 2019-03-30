using System;
using System.Collections.Generic;
using System.Linq;

namespace _01._Concert
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> bandsNameAndMembers = new Dictionary<string, List<string>>();
            Dictionary<string, int> bandsNameAndTime = new Dictionary<string, int>();
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "start of concert")
            {
                string[] tokens = input.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                string bandName = tokens[1];

                if (command == "Add")
                {
                    List<string> bandMembers = tokens[2].Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    bandMembers = bandMembers.Distinct().ToList();
                    if (!bandsNameAndMembers.ContainsKey(bandName))
                    {
                        bandsNameAndMembers.Add(bandName, new List<string>());
                        bandsNameAndMembers[bandName].AddRange(bandMembers);
                    }
                    else
                    {
                        foreach (var member in bandMembers)
                        {
                            if (!bandsNameAndMembers[bandName].Contains(member))
                            {
                                bandsNameAndMembers[bandName].Add(member);
                            }
                        }

                    }
                }
                else if (command == "Play")
                {
                    int time = int.Parse(tokens[2]);
                    if (!bandsNameAndTime.ContainsKey(bandName))
                    {
                        bandsNameAndTime.Add(bandName, 0);

                    }
                    bandsNameAndTime[bandName] += time;
                }


            }
            string band = Console.ReadLine();
            Console.WriteLine("Total time: {0}", bandsNameAndTime.Values.Sum());
            foreach (var kvp in bandsNameAndTime.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine("{0} -> {1}", kvp.Key, kvp.Value);
            }
            foreach (var kvp in bandsNameAndMembers)
            {
                if (kvp.Key == band)
                {
                    Console.WriteLine(kvp.Key);
                    List<string> bandPeople = kvp.Value;
                    foreach (var member in bandPeople)
                    {
                        Console.WriteLine("=> {0}", member);
                    }
                }
            }
        }
    }
}
