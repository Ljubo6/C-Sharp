using System;
using System.Linq;

namespace _1._Sum_Matrix_Elements
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimention = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int r = dimention[0];
            int c = dimention[1];
            int[][] jagedArray = new int[r][];
            InputMatrix(jagedArray);
            Console.WriteLine(r);
            Console.WriteLine(c);
            int sum = 0;
            sum = SumElements(jagedArray, sum);
            Console.WriteLine(sum);
        }

        private static int SumElements(int[][] jagedArray, int sum)
        {
            for (int row = 0; row < jagedArray.Length; row++)
            {
                for (int col = 0; col < jagedArray[row].Length; col++)
                {
                    sum += jagedArray[row][col];
                }
            }

            return sum;
        }

        private static void InputMatrix(int[][] jagedArray)
        {
            for (int row = 0; row < jagedArray.Length; row++)
            {
                jagedArray[row] = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            }
        }
    }
}
