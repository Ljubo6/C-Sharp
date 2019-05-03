using System;
using System.Numerics;

namespace _7._Pascal_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            BigInteger[][] jaggetMatrix = new BigInteger[n][];

            if (n == 1)
            {
                Console.WriteLine("1");
                return;
            }
            else if (n == 2)
            {
                Console.WriteLine("1 1");
                return;
            }
            jaggetMatrix[0] = new BigInteger[] { 1 };
            jaggetMatrix[1] = new BigInteger[] { 1, 1 };
            for (int i = 2; i < n; i++)
            {
                BigInteger[] currentArr = jaggetMatrix[i - 1];
                BigInteger[] arr = new BigInteger[currentArr.Length + 1];
                arr[0] = 1;
                arr[arr.Length - 1] = 1;
                for (int j = 0; j < currentArr.Length - 1; j++)
                {
                    arr[j + 1] = currentArr[j] + currentArr[j + 1];
                }
                jaggetMatrix[i] = arr;
            }
            for (int row = 0; row < jaggetMatrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ", jaggetMatrix[row]));
            }
        }
    }
}
