using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.LongestIncreasingSubsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            int[] length = new int[numbers.Length];
            int[] prev = new int[numbers.Length];

            int maxIndex = 0;
            int maxLength = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                int bestLength = 1;
                int prevIndex = -1;

                for (int j = i - 1; j >= 0 ; j--)
                {
                    if (numbers[i] > numbers[j] && length[j] + 1 >= bestLength)
                    {
                        bestLength = length[j] + 1;
                        prevIndex = j;
                    }                   
                }

                length[i] = bestLength;
                prev[i] = prevIndex;

                if (bestLength > maxLength)
                {
                    maxLength = bestLength;
                    maxIndex = i;
                }
            }

            int[] result = new int[maxLength];

            int index = 0;
            
            while (maxIndex != -1)
            {
                result[index++] = numbers[maxIndex];
                maxIndex = prev[maxIndex];
            }

            Array.Reverse(result);
            Console.WriteLine(string.Join(" ",result));
        }       
    }
}
