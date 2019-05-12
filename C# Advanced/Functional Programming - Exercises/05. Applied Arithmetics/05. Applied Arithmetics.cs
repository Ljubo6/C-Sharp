using System;
using System.Linq;

namespace _05._Applied_Arithmetics
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<int[]> print = p => Console.WriteLine(string.Join(" ",p));
            Func<int[], int[]> addOneFunc = nums => nums.Select(x => x + 1).ToArray();
            Func<int[], int[]> subtractOneFunc = nums => nums.Select(x => x - 1).ToArray();
            Func<int[], int[]> multiplyBy2 = nums => nums.Select(x => x * 1).ToArray();
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "end")
            {
                if (input == "add")
                {
                    numbers = addOneFunc(numbers);
                }
                else if (input == "subtract")
                {
                    numbers = subtractOneFunc(numbers);
                }
                else if (input == "multiply")
                {
                    numbers = multiplyBy2(numbers);
                }
                else if (input == "print")
                {
                    print(numbers);
                }
            }
        }
    }
}
