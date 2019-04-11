using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _03._Nether_Realms
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine()
                .Split(new[] {',',' '},StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim()).ToArray();
            string digitPattern = @"(([-+])?(\d+\.)?\d+)";
            string letterPattern = @"([^\d-.*+\/])";
            SortedDictionary<string, Dictionary<int, double>> damons = new SortedDictionary<string, Dictionary<int, double>>();
            for (int i = 0; i < input.Length; i++)
            {
                string damon = input[i];
                int health = 0;
                double damage = 0;
                MatchCollection digitMatch = Regex.Matches(damon,digitPattern);
                MatchCollection letterMatch = Regex.Matches(damon,letterPattern);
                foreach (Match match in digitMatch)
                {
                    double num = double.Parse(match.Value);
                    damage += num;
                }
                foreach (Match match in letterMatch)
                {
                    char ch = char.Parse(match.Value);
                    health += ch;
                }
                foreach (var symbol in damon)
                {
                    if (symbol == '*')
                    {
                        damage *= 2;
                    }
                    else if (symbol == '/')
                    {
                        damage /= 2;
                    }
                }
                if (!damons.ContainsKey(damon))
                {
                    damons.Add(damon,new Dictionary<int, double>());
                    damons[damon].Add(health,damage);
                }
            }
            foreach (var kvp in damons)
            {
                foreach (var KVP in kvp.Value)
                {
                    Console.WriteLine($"{kvp.Key} - {KVP.Key} health, {KVP.Value:F2} damage");
                }
            }
        }
    }
}
