using System;
using System.Linq;

namespace _02._Sneaking
{
    class Program
    {
        static char[][] jaggedArray;
        static int samTargetRow;
        static int samTargetCol;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            jaggedArray = new char[n][];
            
            FillMatrix();

            char[] commands = Console.ReadLine().ToCharArray();
            foreach (var command in commands)
            {
                EnamiesMoving();
                CheckEnemiesAndSam();
                MoveSam(command);
                CheckNikoladze();
            }
            PrintMatrix();
        }

        private static void CheckNikoladze()
        {
            int colNikoladze;
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                if (jaggedArray[row].Contains('N') && jaggedArray[row].Contains('S'))
                {
                    colNikoladze = Array.IndexOf(jaggedArray[row],'N');
                    jaggedArray[row][colNikoladze] = 'X';
                    Console.WriteLine($"Nikoladze killed!");
                    PrintMatrix();
                }
            }
        }

        private static void MoveSam(char command)
        {
            switch (command)
            {
                case 'U':
                    jaggedArray[samTargetRow][samTargetCol] = '.';
                    jaggedArray[--samTargetRow][samTargetCol] = 'S';

                    break;
                case 'D':
                    jaggedArray[samTargetRow][samTargetCol] = '.';
                    jaggedArray[++samTargetRow][samTargetCol] = 'S';
                    break;
                case 'L':
                    jaggedArray[samTargetRow][samTargetCol] = '.';
                    jaggedArray[samTargetRow][--samTargetCol] = 'S';
                    break;
                case 'R':
                    jaggedArray[samTargetRow][samTargetCol] = '.';
                    jaggedArray[samTargetRow][++samTargetCol] = 'S';
                    break;
                default:
                    break;
            }
        }

        private static void CheckEnemiesAndSam()
        {
            int colSam;
            int colEnemy;
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                if (jaggedArray[row].Contains('b') && jaggedArray[row].Contains('S'))
                {
                    colSam = Array.IndexOf(jaggedArray[row],'S');
                    colEnemy = Array.IndexOf(jaggedArray[row],'b');
                    if (colSam > colEnemy)
                    {
                        jaggedArray[row][colSam] = 'X';
                        Console.WriteLine($"Sam died at {row}, {colSam}");
                        PrintMatrix();
                    }
                }
                if (jaggedArray[row].Contains('d') && jaggedArray[row].Contains('S'))
                {
                    colSam = Array.IndexOf(jaggedArray[row], 'S');
                    colEnemy = Array.IndexOf(jaggedArray[row], 'd');
                    if (colSam < colEnemy)
                    {
                        jaggedArray[row][colSam] = 'X';
                        Console.WriteLine($"Sam died at {row}, {colSam}");
                        PrintMatrix();
                    }
                }
            }
        }

        private static void EnamiesMoving()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                for (int col = 0; col < jaggedArray[row].Length; col++)
                {
                    if (jaggedArray[row][col] == 'b')
                    {
                        if (col == jaggedArray[row].Length - 1)
                        {
                            jaggedArray[row][col] = 'd';
                        }
                        else
                        {
                            jaggedArray[row][col] = '.';
                            jaggedArray[row][++col] = 'b';
                        }
                    }
                    else if (jaggedArray[row][col] == 'd')
                    {
                        if (col == 0)
                        {
                            jaggedArray[row][col] = 'b';
                        }
                        else
                        {
                            jaggedArray[row][col] = '.';
                            jaggedArray[row][--col] = 'd';
                        }
                    }
                }
            }
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                Console.WriteLine(string.Join("",jaggedArray[row]));
            }
            Environment.Exit(0);
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                jaggedArray[row] = Console.ReadLine().ToCharArray() ;
                if (jaggedArray[row].Contains('S'))
                {
                    samTargetRow = row;
                    samTargetCol = Array.IndexOf(jaggedArray[row], 'S');
                }
            }
        }
    }
}
