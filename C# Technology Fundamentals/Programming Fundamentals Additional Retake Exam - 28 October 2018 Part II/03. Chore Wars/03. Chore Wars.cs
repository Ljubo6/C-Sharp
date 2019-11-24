using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _03._Chore_Wars
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            int totalSum = 0;
            Dictionary<string, int> chore = new Dictionary<string, int>();
            string patternDishes = @"(?<=\<)[a-z0-9]+(?=\>)";
            string patternCleaning = @"(?<=\[)[A-Z0-9]+(?=\])";
            string patternLaundry = @"(?<=\{)[\w\W\s]+(?=\})";

            Regex dishesRegex = new Regex(patternDishes);
            Regex cleaningRegex = new Regex(patternCleaning);
            Regex laundryRegex = new Regex(patternLaundry);
            while ((input = Console.ReadLine()) != "wife is happy")
            {
                string str = string.Empty;
                string currentStr = string.Empty;
                if (dishesRegex.IsMatch(input))
                {
                    str = "Doing the dishes";
                    currentStr = dishesRegex.Match(input).ToString(); ;
                }
                else if (cleaningRegex.IsMatch(input))
                {
                    str = "Cleaning the house";
                    currentStr = cleaningRegex.Match(input).ToString();
                }
                else if (laundryRegex.IsMatch(input))
                {
                    str = "Doing the laundry";
                    currentStr = laundryRegex.Match(input).ToString();
                }
                else
                {
                    continue;
                }
                int sum = 0;
                int num = 0;

                for (int i = 0; i < currentStr.Length; i++)
                {
                    if (char.IsDigit(currentStr[i]))
                    {
                        num = int.Parse(currentStr[i].ToString());
                        sum += num;
                    }
                }
                totalSum += sum;
                if (!chore.ContainsKey(str))
                {
                    chore.Add(str, 0);
                }
                chore[str] += sum;
            }

            foreach (var kvp in chore.OrderBy(x => x.Key.Length).Take(1))
            {
                Console.WriteLine("{0} - {1} min.", kvp.Key, kvp.Value);
            }
            foreach (var kvp in chore.OrderBy(x => x.Key.Length).Skip(2).Take(1))
            {
                Console.WriteLine("{0} - {1} min.", kvp.Key, kvp.Value);
            }
            foreach (var kvp in chore.OrderBy(x => x.Key.Length).Skip(1).Take(1))
            {
                Console.WriteLine("{0} - {1} min.", kvp.Key, kvp.Value);
            }

            Console.WriteLine("Total - {0} min.", totalSum);
        }
    }
}
