using System;
using System.Collections.Generic;
using System.Linq;

namespace _6._Bomb_the_Basement
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int r = size[0];
            int c = size[1];
            int[] bomb = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[][] matrix = new int[r][] ;
            GetMatrix(matrix,c);
            BombMatrix(matrix,bomb);
            Collapse(matrix);
            PrintMatrix(matrix);
        }

        private static void Collapse(int[][] matrix)
        {
            Queue<int> queue = new Queue<int>(matrix.Length);
            
            for (int col = 0; col < matrix[0].Length; col++)
            {
                int counter = 0;
                for (int row = 0; row < matrix.Length; row++)
                {
                    if (matrix[row][col] != 1)
                    {
                        queue.Enqueue(matrix[row][col]);
                    }
                    else
                    {
                        counter++;
                    }
                }
                for (int row = 0; row < counter; row++)
                {
                    matrix[row][col] = 1;
                }
                for (int row = counter; row < matrix.Length; row++)
                {
                    matrix[row][col] = queue.Dequeue();
                }
            }
        }

        private static void BombMatrix(int[][] matrix, int[] bomb)
        {
            int targetRow = bomb[0];
            int targetCol = bomb[1];
            int radius = bomb[2];

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    bool isInside = Math.Pow((targetRow - row), 2) + Math.Pow((targetCol - col), 2) <= Math.Pow((radius),2);
                    if (isInside)
                    {
                        matrix[row][col] = 1;
                    }
                }
            }
        }

        private static void GetMatrix(int[][] matrix,int col)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = new int[col];
            }
        }
         
        private static void PrintMatrix(int[][] matrix)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                Console.WriteLine(string.Join("",matrix[row]));
            }
        }
    }
}
