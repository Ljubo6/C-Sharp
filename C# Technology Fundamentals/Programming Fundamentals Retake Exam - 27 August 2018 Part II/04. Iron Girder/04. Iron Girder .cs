using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Iron_Girder
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, int> towntime = new Dictionary<string, int>();
            Dictionary<string, int> townPassengers = new Dictionary<string, int>();
            while ((input = Console.ReadLine()) != "Slide rule")
            {
                string[] tokens = input.Split(':');
                string townName = tokens[0];
                string[] str = tokens[1].Split(new[] {"->"},StringSplitOptions.RemoveEmptyEntries);
                int passengersCount = int.Parse(str[1]);
                if (str[0] != "ambush")
                {
                    int time = int.Parse(str[0]);
                    if (!towntime.ContainsKey(townName) && !townPassengers.ContainsKey(townName))
                    {
                        towntime.Add(townName,0 );
                        townPassengers.Add(townName, 0);
                    }
                    int oldTime = towntime[townName];
                    towntime[townName] = time;
                    if (oldTime < towntime[townName] && oldTime > 0)
                    {
                        towntime[townName] = oldTime;
                    }

                    townPassengers[townName] += passengersCount;
                }
                else
                {
                    if (!towntime.ContainsKey(townName))
                    {
                        continue;
                    }
                    towntime[townName] = 0;
                    townPassengers[townName] -= passengersCount;
                }
            }
            foreach (var kvp in towntime.OrderBy(x => x.Value).ThenBy(x => x.Key))
            {
                string town = kvp.Key;
                if (towntime[town] == 0 || townPassengers[town] == 0)
                {
                    continue;
                }
                Console.WriteLine($"{town} -> Time: {kvp.Value} -> Passengers: {townPassengers[town]}");
            }
        }
    }
}
