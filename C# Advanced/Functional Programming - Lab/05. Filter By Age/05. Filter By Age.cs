using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._Filter_By_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<KeyValuePair<string, int>> people = new List<KeyValuePair<string, int>>();

            for (int i = 0; i < n; i++)
            {
                string[] person = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
                people.Add(new KeyValuePair<string, int>(person[0],int.Parse(person[1])));
            }
            string filter = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            string[] printerPattern = Console.ReadLine().Split(" ");

            people
                .Where(x => filter == "younger" ? x.Value < age : x.Value > age)
                .ToList()
                .ForEach(x => Printer(x,printerPattern));
        }
        public static void Printer(KeyValuePair<string, int> person,string[] printerPattern)
        {
            if (printerPattern.Length == 2)
            {
                Console.WriteLine(printerPattern[0] == "name" 
                    ? $"{person.Key} - {person.Value}" 
                    : $"{person.Value} - {person.Key}");
            }
            else
            {
                Console.WriteLine(printerPattern[0] == "name"
                    ? $"{person.Key}"
                    : $"{person.Value}");
            }
        }
    }
}
