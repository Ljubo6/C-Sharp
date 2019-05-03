using System;
using System.Linq;

namespace _3._Primary_Diagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int[,] matrix = new int[n, n];
            InputMatrix(matrix);
            int primaryDiagonalSum = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int col = row;
                primaryDiagonalSum += matrix[row, col];
            }
            Console.WriteLine(primaryDiagonalSum);
        }

        private static void InputMatrix(int[,] matrix)
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
