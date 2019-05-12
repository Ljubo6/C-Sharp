using System;
using System.Linq;

namespace _03._Custom_Min_Function
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int> output = x => Console.WriteLine(x);
            int[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Func<int[], int> minNumFinc = x => input.Min();
            int numMin = minNumFinc(input);
            output(numMin);
        }
    }
}
