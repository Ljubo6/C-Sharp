using System;
using System.Linq;

namespace _3._Maximal_Sum
{
    class Program
    {
        static int[,] matrix;
        static int maxRow;
        static int maxCol;

        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int r = size[0];
            int c = size[1];
            matrix = new int[r, c];

            FillMAtrix();
            int totalSum = int.MinValue;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (IsValidSubmatrix(row, col))
                    {
                        int targetRow = row;
                        int targetCol = col;
                        int currentSum = GetCurrentSum(targetRow, targetCol);
                        if (totalSum < currentSum)
                        {
                            totalSum = currentSum;
                            maxRow = targetRow;
                            maxCol = targetCol;
                        }
                    }
                }
            }
            Console.WriteLine($"Sum = {totalSum}");
            PrintMaxSubmatrix();
        }

        private static void PrintMaxSubmatrix()
        {
            for (int row = maxRow; row < maxRow + 3; row++)
            {
                for (int col = maxCol; col < maxCol + 3; col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }
                Console.WriteLine();
            }
        }

        private static int GetCurrentSum(int targetRow, int targetCol)
        {
            int sum = 0;
            for (int row = targetRow; row < targetRow + 3; row++)
            {
                for (int col = targetCol; col < targetCol + 3; col++)
                {
                    sum += matrix[row, col];
                }
            }
            return sum;
        }

        private static bool IsValidSubmatrix(int row, int col)
        {
            return row + 2 < matrix.GetLength(0) && col + 2 < matrix.GetLength(1);
        }

        private static void FillMAtrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] line = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
    }
}
