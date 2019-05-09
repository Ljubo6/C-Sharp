using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Average_Student_Grades
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            Dictionary<string, List<double>> dict = new Dictionary<string, List<double>>();
            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string name = input[0];
                double grade = double.Parse(input[1]);

                if (!dict.ContainsKey(name))
                {
                    dict.Add(name,new List<double>());
                }
                dict[name].Add(grade);
            }
            foreach (var kvp in dict)
            {
                Console.WriteLine($"{kvp.Key} -> {string.Join(" ",kvp.Value.Select(i => i.ToString("F2")))} (avg: {kvp.Value.Average():F2})");
            }
        }
    }
}
