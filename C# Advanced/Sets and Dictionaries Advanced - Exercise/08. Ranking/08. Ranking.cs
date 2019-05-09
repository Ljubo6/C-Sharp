using System;
using System.Collections.Generic;
using System.Linq;

namespace _08._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> contestPassword = new Dictionary<string, string>();
            Dictionary<string, Dictionary<string, int>> nameContestPoints = new Dictionary<string, Dictionary<string, int>>();
            string input = string.Empty;
            while ((input = Console.ReadLine()) != "end of contests")
            {
                string[] tokens = input.Split(':');
                string contest = tokens[0];
                string password = tokens[1];
                if (!contestPassword.ContainsKey(contest))
                {
                    contestPassword.Add(contest,string.Empty);
                }
                contestPassword[contest] = password;
            }
            input = string.Empty;
            while ((input = Console.ReadLine()) != "end of submissions")
            {
                string[] tokens = input.Split("=>");
                string contest = tokens[0];
                string password = tokens[1];
                string name = tokens[2];
                int points = int.Parse(tokens[3]);

                if (contestPassword.ContainsKey(contest) && contestPassword[contest] == password)
                {
                    if (!nameContestPoints.ContainsKey(name))
                    {
                        nameContestPoints.Add(name,new Dictionary<string, int>());
                    }
                    if (!nameContestPoints[name].ContainsKey(contest))
                    {
                        nameContestPoints[name][contest] = 0;
                    }
                    if (points > nameContestPoints[name][contest])
                    {
                        nameContestPoints[name][contest] = points;
                    }
                }
            }
            var tempDict = nameContestPoints.OrderByDescending(x => x.Value.Values.Sum()).Take(1).ToDictionary(x => x.Key,x => x.Value);
            foreach (var kvp in tempDict)
            {
                Console.WriteLine($"Best candidate is {kvp.Key} with total {kvp.Value.Values.Sum()} points.");
            }
            Console.WriteLine("Ranking: ");
            foreach (var kvp in nameContestPoints.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key}");
                var currentDict = kvp.Value;
                foreach (var KVP in currentDict.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine($"#  {KVP.Key} -> {KVP.Value}");
                }
            }
        }
    }
}
