using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _03._Football_League
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            string key = Console.ReadLine();
            Dictionary<string, long> standingTtable = new Dictionary<string, long>();
            Dictionary<string, long> standingTtableWithGoals = new Dictionary<string, long>();
            while ((input = Console.ReadLine()) != "final")
            {
                string[] tokens = input.Split();

                string home = tokens[0];
                string guest = tokens[1];
                string score = tokens[2];

                string[] goals = score.Split(':');
                int homeGoals = int.Parse(goals[0]);
                int guestGoals = int.Parse(goals[1]);

                int guestPoints = 0;
                int homePoints = 0;
                if (homeGoals > guestGoals)
                {
                    homePoints = 3;
                    guestPoints = 0;
                }
                else if (homeGoals < guestGoals)
                {
                    homePoints = 0;
                    guestPoints = 3;
                }
                else
                {
                    homePoints = 1;
                    guestPoints = 1;
                }
                int startHomeIndex = home.IndexOf(key);
                int endHomeIndex = home.LastIndexOf(key);
                string homeTeam = home.Substring(startHomeIndex + key.Length, endHomeIndex - startHomeIndex - key.Length);
                char[] homeTeamArray = homeTeam.ToCharArray();
                Array.Reverse(homeTeamArray);
                homeTeam = new String(homeTeamArray).ToUpper();

                int startGuestIndex = guest.IndexOf(key);
                int endGuestIndex = guest.LastIndexOf(key);
                string guestTeam = guest.Substring(startGuestIndex + key.Length, endGuestIndex - startGuestIndex - key.Length);
                char[] guestTeamArray = guestTeam.ToCharArray();
                Array.Reverse(guestTeamArray);
                guestTeam = new String(guestTeamArray).ToUpper();

                if (!standingTtable.ContainsKey(homeTeam))
                {
                    standingTtable.Add(homeTeam, homePoints);
                    standingTtableWithGoals.Add(homeTeam, homeGoals);
                }
                else
                {
                    standingTtable[homeTeam] += homePoints;
                    standingTtableWithGoals[homeTeam] += homeGoals;
                }
                if (!standingTtable.ContainsKey(guestTeam))
                {
                    standingTtable.Add(guestTeam, guestPoints);
                    standingTtableWithGoals.Add(guestTeam, guestGoals);
                }
                else
                {
                    standingTtable[guestTeam] += guestPoints;
                    standingTtableWithGoals[guestTeam] += guestGoals;
                }
            }
            int place = 1;
            Console.WriteLine("League standings:");
            foreach (var team in standingTtable.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{place}. {team.Key} {team.Value}");
                place++;
            }
            Console.WriteLine("Top 3 scored goals:");
            foreach (var team in standingTtableWithGoals.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Take(3))
            {
                Console.WriteLine($"- {team.Key} -> {team.Value}");
            }
            Console.WriteLine();
        }
    }
}
