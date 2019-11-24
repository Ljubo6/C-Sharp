using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _03._Star_Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, int> attackedPlanets = new Dictionary<string, int>();
            Dictionary<string, int> destroyedPlanets = new Dictionary<string, int>();
            for (int i = 0; i < n; i++)
            {
                string message = Console.ReadLine();
                int key = EncriptKey(n, message);
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < message.Length; j++)
                {
                    char ch = (char)(message[j] - key);
                    sb.Append(ch);
                }
                string encriptedMessage = sb.ToString();
                string pattern = @"([^@,\-!:>]*)@(?<planet>[A-Za-z]+)([^@,\-!:>]*):(?<population>\d+)([^@,\-!:>]*)!(?<action>[AD])!([^@,\-!:>]*)->(?<soldierCount>\d+)([^@,\-!:>]*)";
                Regex regex = new Regex(pattern);
                MatchCollection matches = regex.Matches(encriptedMessage);
                foreach (Match match in matches)
                {
                    string planet = match.Groups["planet"].Value;
                    string action = match.Groups["action"].Value;
                    if (action == "A")
                    {
                        if (!attackedPlanets.ContainsKey(planet))
                        {
                            attackedPlanets.Add(planet,0);
                        }
                        attackedPlanets[planet]++; 
                    }
                    else if (action == "D")
                    {
                        if (!destroyedPlanets.ContainsKey(planet))
                        {
                            destroyedPlanets.Add(planet,0);
                        }
                        destroyedPlanets[planet]++;
                    }
                }

            }
            Console.WriteLine($"Attacked planets: {attackedPlanets.Values.Count}");
            foreach (var attack in attackedPlanets.OrderBy(x => x.Key))
            {
                Console.WriteLine($"-> {attack.Key}");
            }
            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Values.Count}");
            foreach (var destroy in destroyedPlanets.OrderBy(x => x.Key))
            {
                Console.WriteLine($"-> {destroy.Key}");
            }
        }
        private static int EncriptKey(int n,string message)
        {
            int count = 0;

            string pattern = @"[S,s,T,t,A,a,R,r]";
            Regex regex = new Regex(pattern);
            count = 0;
            for (int j = 0; j < message.Length; j++)
            {
                if (regex.IsMatch(message[j].ToString()))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
