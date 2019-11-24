using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._ForceBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> forceUser = new Dictionary<string, string>();
            Dictionary<string, int> forceSide = new Dictionary<string, int>();
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "Lumpawaroo")
            {
                if (input.Contains(" | "))
                {
                    string[] tokens = input.Split(new[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                    string side = tokens[0];
                    string name = tokens[1];
                    if (!forceUser.ContainsKey(name))
                    {
                        forceUser.Add(name,side);
                        if (!forceSide.ContainsKey(side))
                        {
                            forceSide.Add(side,0);
                        }
                        forceSide[side]++;
                    }
                }
                else if (input.Contains(" -> "))
                {
                    string[] tokens = input.Split(new[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    string name = tokens[0];
                    string side = tokens[1];

                    if (!forceUser.ContainsKey(name))
                    {
                        forceUser.Add(name,side);
                        if (!forceSide.ContainsKey(side))
                        {
                            forceSide.Add(side,0);
                        }
                        forceSide[side]++;
                    }
                    else
                    {
                        forceSide[forceUser[name]]--;
                        if (!forceSide.ContainsKey(side))
                        {
                            forceSide.Add(side,0);
                        }
                        forceSide[side]++;
                        forceUser[name] = side;
                    }
                    Console.WriteLine($"{name} joins the {side} side!");
                }
            }
            foreach (var forseSide in forceSide.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                if (forseSide.Value > 0)
                {
                    Console.WriteLine($"Side: {forseSide.Key}, Members: {forseSide.Value}");
                    foreach (var forseUser in forceUser.Where(x => x.Value == forseSide.Key).OrderBy(x => x.Key))
                    {
                        Console.WriteLine($"! {forseUser.Key}");
                    }
                }
            }
        }
    }
}
