using System;
using System.Linq;

namespace _5._Square_With_Maximum_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] matrixSize = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int r = matrixSize[0];
            int c = matrixSize[1];
            int[][] jaggedMatrix = new int[r][];

            InputMatrix(jaggedMatrix);
            int maxSum = int.MinValue;
            int sum = 0;
            int targetRow = 0;
            int targetCol = 0;
            for (int row = 0; row < jaggedMatrix.Length - 1; row++)
            {
                for (int col = 0; col < jaggedMatrix[row].Length - 1; col++)
                {
                    sum = jaggedMatrix[row][col] + jaggedMatrix[row][col + 1] + jaggedMatrix[row + 1][col] + jaggedMatrix[row + 1][col + 1];
                    if (maxSum < sum)
                    {
                        maxSum = sum;
                        targetRow = row;
                        targetCol = col;
                    }
                }
            }

            PrintMaxSubMatrix(jaggedMatrix, targetRow, targetCol);
            Console.WriteLine(maxSum);
        }

        private static void PrintMaxSubMatrix(int[][] jaggedMatrix, int targetRow, int targetCol)
        {
            for (int row = targetRow; row < targetRow + 2; row++)
            {
                for (int col = targetCol; col < targetCol + 2; col++)
                {
                    Console.Write($"{jaggedMatrix[row][col]} ");
                }
                Console.WriteLine();
            }
        }

        private static void InputMatrix(int[][] jaggedMatrix)
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            }
        }
    }
}
