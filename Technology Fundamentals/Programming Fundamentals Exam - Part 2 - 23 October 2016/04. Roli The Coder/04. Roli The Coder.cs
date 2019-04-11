using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Roli_The_Coder
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> idAndEventName = new Dictionary<int, string>();
            Dictionary<string, List<string>> eventNameAndParticipant = new Dictionary<string, List<string>>();
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "Time for Code")
            {
                
                string[] tokens = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                int id = int.Parse(tokens[0]);
                string eventName = tokens[1];
                if (!eventName.StartsWith("#"))
                {
                    continue;
                }
                if (!idAndEventName.ContainsKey(id))
                {
                    idAndEventName.Add(id, eventName);
                    eventNameAndParticipant.Add(eventName,new List<string>());
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        if (!eventNameAndParticipant[eventName].Contains(tokens[i]))
                        {
                            eventNameAndParticipant[eventName].Add(tokens[i]);
                        }
                    }
                }
                else
                {
                    if (idAndEventName[id] == eventName)
                    {
                        if (eventNameAndParticipant.ContainsKey(eventName))
                        {
                            for (int i = 2; i < tokens.Length; i++)
                            {
                                if (!eventNameAndParticipant[eventName].Contains(tokens[i]))
                                {
                                    eventNameAndParticipant[eventName].Add(tokens[i]);
                                }
                            }
                        }
                    }
                }

            }
            foreach (var kvp in eventNameAndParticipant.OrderByDescending(x => x.Value.Count).ThenBy(x => x.Key))
            {
                string name = kvp.Key.Substring(1);
                Console.WriteLine($"{name} - {kvp.Value.Count}");
                foreach (var participant in kvp.Value.OrderBy(x => x))
                {
                    Console.WriteLine(participant);
                }
            }
        }
    }
}
