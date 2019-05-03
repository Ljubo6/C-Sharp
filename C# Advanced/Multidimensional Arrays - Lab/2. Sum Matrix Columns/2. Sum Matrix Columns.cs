using System;
using System.Linq;

namespace _2._Sum_Matrix_Columns
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] dimention = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            int r = dimention[0];
            int c = dimention[1];
            int[,] matrix = new int [r,c];
            InputMatrix(matrix);
            int sum = 0;
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                sum = 0;
                for (int row = 0; row < matrix.GetLength(0); row++)
                {
                    int element = matrix[row,col];
                    sum += element;
                }
                Console.WriteLine(sum);
            }
        }
        private static void InputMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                int[] input = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = input[col];
                }
            }
        }
    }
}
