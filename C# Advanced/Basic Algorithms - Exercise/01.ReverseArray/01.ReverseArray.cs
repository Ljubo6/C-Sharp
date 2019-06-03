using System;
using System.Linq;

namespace _01.ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

            PrintIntReverseOrder(arr, arr.Length - 1);
        }

        private static void PrintIntReverseOrder(int[] arr, int index)
        {
            if (index < 0)
            {
                return;
            }

            Console.Write(arr[index] + " ");
            PrintIntReverseOrder(arr, index - 1);
        }
    }
}
