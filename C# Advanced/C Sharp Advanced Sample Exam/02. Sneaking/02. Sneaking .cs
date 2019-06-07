using System;
using System.Linq;

namespace _02._Sneaking
{
    class Program
    {
        static char[][] matrix;
        static int samTargetRow;
        static int samTargetCol;
        static void Main(string[] args)
        {
            int rowSize = int.Parse(Console.ReadLine());
            matrix = new char[rowSize][];

            FillMatrix(rowSize);

            char[] commands = Console.ReadLine().ToCharArray();

            foreach (var command in commands)
            {
                UpdateEnamies();
                CheckEnemies();
                MoveSam(command);
                CheckNikoladze();
            }
        }

        private static void MoveSam(char command)
        {
            switch (command)
            {
                case 'U':
                    matrix[samTargetRow][samTargetCol] = '.';
                    matrix[--samTargetRow][samTargetCol] = 'S';
                    break;
                case 'D':
                    matrix[samTargetRow][samTargetCol] = '.';
                    matrix[++samTargetRow][samTargetCol] = 'S';
                    break;
                case 'L':
                    matrix[samTargetRow][samTargetCol] = '.';
                    matrix[samTargetRow][--samTargetCol] = 'S';
                    break;
                case 'R':
                    matrix[samTargetRow][samTargetCol] = '.';
                    matrix[samTargetRow][++samTargetCol] = 'S';
                    break;
                default:
                    break;
            }
        }

        private static void CheckNikoladze()
        {
            int colNikoladze;
            for (int row = 0; row < matrix.Length; row++)
            {
                if (matrix[row].Contains('N') && matrix[row].Contains('S'))
                {
                    colNikoladze = Array.IndexOf(matrix[row], 'N');
                    matrix[row][colNikoladze] = 'X';
                    Console.WriteLine($"Nikoladze killed!");
                    PrintMatrix();
                }
            }
        }

        private static void CheckEnemies()
        {
            int colEnemy;
            int colSam;
            for (int row = 0; row < matrix.Length; row++)
            {
                if (matrix[row].Contains('b') && matrix[row].Contains('S'))
                {
                    colEnemy = Array.IndexOf(matrix[row], 'b');
                    colSam = Array.IndexOf(matrix[row], 'S');
                    if (colEnemy < colSam)
                    {
                        matrix[row][colSam] = 'X';
                        Console.WriteLine($"Sam died at {row}, {colSam}");
                        PrintMatrix();
                    }
                }
                else if (matrix[row].Contains('d') && matrix[row].Contains('S'))
                {
                    colEnemy = Array.IndexOf(matrix[row], 'd');
                    colSam = Array.IndexOf(matrix[row], 'S');
                    if (colEnemy > colSam)
                    {
                        matrix[row][colSam] = 'X';
                        Console.WriteLine($"Sam died at {row}, {colSam}");
                        PrintMatrix();
                    }
                }
            }
        }

        private static void UpdateEnamies()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == 'b')
                    {
                        if (col == matrix[row].Length - 1)
                        {
                            matrix[row][col] = 'd';
                        }
                        else
                        {
                            matrix[row][col] = '.';
                            matrix[row][++col] = 'b';
                        }
                    }
                    else if (matrix[row][col] == 'd')
                    {
                        if (col == 0)
                        {
                            matrix[row][col] = 'b';
                        }
                        else
                        {
                            matrix[row][col] = '.';
                            matrix[row][--col] = 'd';
                        }
                    }
                }
            }
        }

        private static void PrintMatrix()
        {
            foreach (var element in matrix)
            {
                Console.WriteLine(string.Join("", element));
            }
            Environment.Exit(0);
        }

        private static void FillMatrix(int rowSize)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
                if (matrix[row].Contains('S'))
                {
                    samTargetRow = row;
                    samTargetCol = Array.IndexOf(matrix[row], 'S');
                }
            }
        }
    }
}
