using System;
using System.Collections.Generic;
using System.Linq;

namespace _04._Find_Evens_or_Odds
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<int>> print = x => Console.WriteLine(string.Join(" ",x));
            Predicate<int> predicate;
            int[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            string predicateName = Console.ReadLine();
            int start = input[0];
            int end = input[1];
            List<int> collection = new List<int>();
            for (int i = start; i <= end; i++)
            {
                collection.Add(i);
            }
            predicate = GetPredicate(predicateName);

            List<int> outputList = collection.FindAll(predicate);

            print(outputList);
        }

        private static Predicate<int> GetPredicate(string predicateName)
        {
            if (predicateName == "odd")
            {
                return p => p % 2 != 0;
            }
            else if (predicateName == "even")
            {
                return p => p % 2 == 0;
            }
            return null;
        }
    }
}
