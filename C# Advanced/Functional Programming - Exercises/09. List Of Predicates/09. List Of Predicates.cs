using System;
using System.Collections.Generic;
using System.Linq;

namespace _09._List_Of_Predicates
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<List<int>> print = x => Console.WriteLine(string.Join(" ",x));
            Predicate<int> predicate;
            int n = int.Parse(Console.ReadLine());
            int[] dividers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            List<int> resultList = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                resultList.Add(i);
            }
            for (int i = 0; i < dividers.Length; i++)
            {
                predicate = GetPredicate(dividers[i]);

                resultList = resultList.FindAll(predicate);
            }
            print(resultList);
            
        }

        private static Predicate<int> GetPredicate(int i)
        {
            return p => p % i == 0;
        }
    }
}
