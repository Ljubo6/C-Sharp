using System;
using System.Linq;

namespace _5._Snake_Moves
{
    class Program
    {
        static char[,] matrix;
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int r = size[0];
            int c = size[1];
            matrix = new char[r, c];

            string str = Console.ReadLine();

            int count = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (count == str.Length)
                    {
                        count = 0;
                    }
                    matrix[row, col] = str[count++];
                }
            }

            PrintMatrix();
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
