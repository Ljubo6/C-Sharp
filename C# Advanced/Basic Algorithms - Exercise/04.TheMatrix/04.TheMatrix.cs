using System;
using System.Linq;

namespace _04.TheMatrix
{
    public struct Area
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Size { get; set; }
    }
    class Program
    {
        public static int rows;
        public static int cols;
        static void Main(string[] args)
        {
            int[] tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            rows = tokens[0];
            cols = tokens[1];

            char[][] matrix = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Console.ReadLine().Replace(" ", "").ToCharArray();
            }

            char fillChar = char.Parse(Console.ReadLine());

            tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int startRow = tokens[0];
            int startCol = tokens[1];

            char toBeReplaceed = matrix[startRow][startCol];

            ColorMatrix(matrix, startRow, startCol, fillChar, toBeReplaceed);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i][j]);
                }
                Console.WriteLine();
            }
        }

        private static void ColorMatrix(char[][] matrix, int r, int c, char fillChar, char toBeReplaceed)
        {
            if (!IsInBounds(r, c) || matrix[r][c] == fillChar || matrix[r][c] != toBeReplaceed)
            {
                return;
            }

            matrix[r][c] = fillChar;

            ColorMatrix(matrix, r + 1, c, fillChar, toBeReplaceed);
            ColorMatrix(matrix, r - 1, c, fillChar, toBeReplaceed);
            ColorMatrix(matrix, r, c + 1, fillChar, toBeReplaceed);
            ColorMatrix(matrix, r, c - 1, fillChar, toBeReplaceed);
        }

        private static bool IsInBounds(int r, int c)
        {
            return r < rows && r >= 0 && c < cols && c >= 0;
        }
    }
}
