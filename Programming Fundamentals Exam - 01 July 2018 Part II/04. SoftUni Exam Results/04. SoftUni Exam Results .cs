using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._SoftUni_Exam_Results
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            Dictionary<string, int> nameResult = new Dictionary<string, int>();
            Dictionary<string, int> languagePoints = new Dictionary<string, int>();
            while ((input = Console.ReadLine()) != "exam finished")
            {
                string[] tokens = input.Split('-');
                string name = tokens[0];
                if (tokens[1] != "banned")
                {
                    string programLanguage = tokens[1];
                    int point = int.Parse(tokens[2]);
                    if (!nameResult.ContainsKey(name))
                    {
                        nameResult.Add(name,0);
                        
                    }
                    if (nameResult[name] < point)
                    {
                        nameResult[name] = point;
                    }
                    if (!languagePoints.ContainsKey(programLanguage))
                    {
                        languagePoints.Add(programLanguage, 0);
                    }
                    languagePoints[programLanguage]++;
                }
                else
                {
                    nameResult.Remove(name);
                }
            }
            Console.WriteLine("Results:");
            foreach (var kvp in nameResult.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }
            Console.WriteLine("Submissions:");
            foreach (var kvp in languagePoints.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                Console.WriteLine($"{kvp.Key} | {kvp.Value}");
            }
        }
    }
}
