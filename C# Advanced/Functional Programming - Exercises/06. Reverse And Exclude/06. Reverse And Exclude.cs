using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Reverse_And_Exclude
{
    class Program
    {
        static void Main(string[] args)
        {
            

            List<int> inputNumbers = Console.ReadLine().Split().Select(int.Parse).ToList();
            int divNumber = int.Parse(Console.ReadLine());

            Action<List<int>> print = p => Console.WriteLine(string.Join(" ",p));
            Action<List<int>> reverseFunc = nums => nums.Reverse();
            Func<List<int>, List<int>> removeNumbersFunc = numbers => numbers.Where(x => x % divNumber != 0).ToList();

            reverseFunc(inputNumbers);
            inputNumbers = removeNumbersFunc(inputNumbers);
            print(inputNumbers);
        }
    }
}
