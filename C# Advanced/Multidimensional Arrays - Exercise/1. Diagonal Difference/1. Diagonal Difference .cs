using System;
using System.Linq;

namespace _1._Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            FillMatrix(matrix);

            int primaryDiagonalSum = GetPrimarySum(matrix);

            int secontaryDiagonalSum = GetSecondarySum(matrix);

            int diff = Math.Abs(primaryDiagonalSum - secontaryDiagonalSum);
            Console.WriteLine(diff);
        }

        private static int GetSecondarySum(int[,] matrix)
        {
            int col = 0;
            int sum = 0;
            for (int row = matrix.GetLength(0) - 1; row >= 0; row--)
            {

                sum += matrix[row, col];
                col++;
            }
            return sum;
        }

        private static int GetPrimarySum(int[,] matrix)
        {
            int col = 0;
            int sum = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                col = row;
                sum += matrix[row, col];
            }
            return sum;
        }

        private static void FillMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }
    }
}
