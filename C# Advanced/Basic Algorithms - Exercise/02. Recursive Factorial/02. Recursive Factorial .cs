using System;

namespace _02._Recursive_Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine(CalculateFactorial(number));
        }

        private static long CalculateFactorial(int number)
        {
            if (number == 0)
            {
                return 1;
            }
            return number * CalculateFactorial(number - 1);
        }
    }
}
