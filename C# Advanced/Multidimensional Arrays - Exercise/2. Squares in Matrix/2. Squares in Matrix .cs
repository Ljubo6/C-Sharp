using System;
using System.Linq;

namespace _2._Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int r = size[0];
            int c = size[1];

            char[,] matrix = new char[r, c];

            FillMatrix(matrix);
            int totalSum = CalculateTotalSum(matrix);

            Console.WriteLine(totalSum);


        }

        private static int CalculateTotalSum(char[,] matrix)
        {
            int totalSum = 0;
            for (int row = 0; row < matrix.GetLength(0) - 1; row++)
            {
                for (int col = 0; col < matrix.GetLength(1) - 1; col++)
                {
                    if (matrix[row, col] == matrix[row, col + 1] && matrix[row, col] == matrix[row + 1, col] && matrix[row + 1, col] == matrix[row + 1, col + 1])
                    {
                        totalSum++;
                    }
                }
            }
            return totalSum;
        }

        private static void FillMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                char[] line = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(char.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = line[col];
                }
            }
        }
    }
}
