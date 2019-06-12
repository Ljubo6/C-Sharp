using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Tron_Racers
{
    class Program
    {
        static char[][] jaggedMatrix;
        static int firstPlayerRow;
        static int firstPlayerCol;
        static int secondPlayerRow;
        static int secondPlayerCol;
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            jaggedMatrix = new char[size][];

            FillMatrix();

            while (true)
            {
                string[] input = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string commandLeft = input[0];
                string commandRight = input[1];
                switch (commandLeft)
                {
                    case "up":
                        if (firstPlayerRow == 0)
                        {
                            firstPlayerRow = size - 1;
                            MoveFirst();
                        }
                        else
                        {
                            firstPlayerRow--;
                            MoveFirst();
                        }
                        break;
                    case "down":
                        if (firstPlayerRow == size - 1)
                        {
                            firstPlayerRow = 0;
                            MoveFirst();
                        }
                        else
                        {
                            firstPlayerRow++;
                            MoveFirst();
                        }
                        break;
                    case "left":

                        if (firstPlayerCol == 0)
                        {
                            firstPlayerCol = size - 1;
                            MoveFirst();
                        }
                        else
                        {
                            firstPlayerCol--;
                            MoveFirst();
                        }
                        break;
                    case "right":
                        if (firstPlayerCol == size - 1)
                        {
                            firstPlayerCol = 0;
                            MoveFirst();
                        }
                        else
                        {
                            firstPlayerCol++;
                            MoveFirst();
                        }
                        break;
                    default:
                        break;
                }
                if (jaggedMatrix[firstPlayerRow][firstPlayerCol] == 'x' ||
                    jaggedMatrix[secondPlayerRow][secondPlayerCol] == 'x')
                {
                    break;
                }
                switch (commandRight)
                {
                    case "up":
                        if (secondPlayerRow == 0)
                        {
                            secondPlayerRow = size - 1;
                            MoveSecond();
                        }
                        else
                        {
                            secondPlayerRow--;
                            MoveSecond();
                        }
                        break;
                    case "down":
                        if (secondPlayerRow == size - 1)
                        {
                            secondPlayerRow = 0;
                            MoveSecond();
                        }
                        else
                        {
                            secondPlayerRow++;
                            MoveSecond();
                        }
                        break;
                    case "left":

                        if (secondPlayerCol == 0)
                        {
                            secondPlayerCol = size - 1;
                            MoveSecond();
                        }
                        else
                        {
                            secondPlayerCol--;
                            MoveSecond();
                        }
                        break;
                    case "right":
                        if (secondPlayerCol == size - 1)
                        {
                            secondPlayerCol = 0;
                            MoveSecond();
                        }
                        else
                        {
                            secondPlayerCol++;
                            MoveSecond();
                        }
                        break;
                    default:
                        break;
                }
                if (jaggedMatrix[firstPlayerRow][firstPlayerCol] == 'x' ||
                    jaggedMatrix[secondPlayerRow][secondPlayerCol] == 'x')
                {
                    break;
                }
            }
            PrintMatrix();
        }
        private static void MoveSecond()
        {
            if (jaggedMatrix[secondPlayerRow][secondPlayerCol] == 'f')
            {
                jaggedMatrix[secondPlayerRow][secondPlayerCol] = 'x';
            }
            else
            {
                jaggedMatrix[secondPlayerRow][secondPlayerCol] = 's';
            }

        }

        private static void MoveFirst()
        {
            if (jaggedMatrix[firstPlayerRow][firstPlayerCol] == 's')
            {
                jaggedMatrix[firstPlayerRow][firstPlayerCol] = 'x';

            }
            else
            {
                jaggedMatrix[firstPlayerRow][firstPlayerCol] = 'f';
            }

        }
        private static void PrintMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                Console.WriteLine(string.Join("", jaggedMatrix[row]));
            }
        }

        private static void FillMatrix()
        {
            for (int i = 0; i < jaggedMatrix.Length; i++)
            {
                jaggedMatrix[i] = Console.ReadLine().ToCharArray();
                if (jaggedMatrix[i].Contains('f'))
                {
                    firstPlayerRow = i;
                    firstPlayerCol = Array.IndexOf(jaggedMatrix[i], 'f');
                }
                if (jaggedMatrix[i].Contains('s'))
                {
                    secondPlayerRow = i;
                    secondPlayerCol = Array.IndexOf(jaggedMatrix[i], 's');
                }
            }
        }
    }
}
