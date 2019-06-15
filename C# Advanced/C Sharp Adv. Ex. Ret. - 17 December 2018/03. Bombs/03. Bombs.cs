using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Bombs
{
    class Program
    {
        static int[][] jaggedMatrix;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            jaggedMatrix = new int[n][];
            FillMatrix();
            string[] coordinates = Console.ReadLine().Split(" ");
            Queue<string> bombs = new Queue<string>(coordinates);
            while (bombs.Any())
            {
                int[] cellAddres = bombs.Dequeue().Split(",").Select(int.Parse).ToArray();
                int r = cellAddres[0];
                int c = cellAddres[1];
                if (jaggedMatrix[r][c] > 0)
                {
                    BombExplode(r, c);
                    jaggedMatrix[r][c] = 0;
                }                
            }
            int aliveCount = 0;
            long sum = 0;
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                for (int col = 0; col < jaggedMatrix[row].Length; col++)
                {
                    if (jaggedMatrix[row][col] > 0)
                    {
                        aliveCount++;
                        sum += jaggedMatrix[row][col];
                    }
                }
            }
            Console.WriteLine($"Alive cells: {aliveCount}");
            Console.WriteLine($"Sum: {sum}");
            PrintMatrix();
        }

        private static void BombExplode(int row,int col)
        {
            int bombPower = jaggedMatrix[row][col];
            if (IsInside(row - 1, col - 1)) jaggedMatrix[row - 1][col - 1] -= bombPower;
            if (IsInside(row - 1, col)) jaggedMatrix[row - 1][col] -= bombPower;
            if (IsInside(row - 1, col + 1)) jaggedMatrix[row - 1][col + 1] -= bombPower;
            if (IsInside(row, col - 1)) jaggedMatrix[row][col - 1] -= bombPower;
            if (IsInside(row, col + 1)) jaggedMatrix[row][col + 1] -= bombPower;
            if (IsInside(row + 1, col - 1)) jaggedMatrix[row + 1][col - 1] -= bombPower;
            if (IsInside(row + 1, col)) jaggedMatrix[row + 1][col] -= bombPower;
            if (IsInside(row + 1, col + 1)) jaggedMatrix[row + 1][col + 1] -= bombPower;

        }

        private static bool IsInside(int r, int c)
        {
            return r >= 0 && r < jaggedMatrix.Length 
                && c >= 0 && c < jaggedMatrix[r].Length 
                && jaggedMatrix[r][c] > 0;
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ",jaggedMatrix[row]));
            }
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split().Select(int.Parse).ToArray(); 
            }
        }
    }
}
