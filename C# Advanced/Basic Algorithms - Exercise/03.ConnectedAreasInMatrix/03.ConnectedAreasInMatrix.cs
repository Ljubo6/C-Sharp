using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.ConnectedAreasInMatrix
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
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());

            char[][] matrix = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }

            List<Area> areas = new List<Area>();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if ((matrix[r][c]) == '-')
                    {
                        Area area = new Area();
                        area.Row = r;
                        area.Col = c;
                        int size = 0;
                        FindAreaSize(matrix, r, c, ref size);
                        area.Size = size;
                        areas.Add(area);
                    }
                }
            }
            Console.WriteLine($"Total areas found: {areas.Count}");
            int counter = 1;
            foreach (var area in areas.OrderByDescending(a => a.Size).ThenBy(a => a.Row).ThenBy(a => a.Col))
            {
                Console.WriteLine($"Area #{counter++} at ({area.Row}, {area.Col}), size: {area.Size}");
            }
        }

        private static void FindAreaSize(char[][] matrix, int r, int c, ref int size)
        {
            if (!IsInBounds(r, c) || matrix[r][c] == '*'
                || matrix[r][c] == 'X')
            {
                return;
            }
            size++;

            matrix[r][c] = 'X';

            FindAreaSize(matrix, r + 1, c, ref size);
            FindAreaSize(matrix, r - 1, c, ref size);
            FindAreaSize(matrix, r, c + 1, ref size);
            FindAreaSize(matrix, r, c - 1, ref size);
        }

        private static bool IsInBounds(int r, int c)
        {
            return r < rows && r >= 0 && c < cols && c >= 0;
        }
    }
}
