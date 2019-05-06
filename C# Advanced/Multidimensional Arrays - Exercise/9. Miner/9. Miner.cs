using System;
using System.Linq;

namespace _9._Miner
{
    class Program
    {
        static char[][] matrix;
        static int currentRow;
        static int currentCol;
        static int coalsCount = 0;
        static bool IsCollect;
        static bool IsDead;
        static int count = 0;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] commands = Console.ReadLine().Split();
            matrix = new char[n][];

            FillMatrix();
            MoveMiner(commands);

            if (IsCollect)
            {
                Console.WriteLine($"You collected all coals! ({currentRow}, {currentCol})");
                return;
            }
            if (IsDead)
            {
                Console.WriteLine($"Game over! ({currentRow}, {currentCol})");
                return;
            }
            Console.WriteLine($"{coalsCount -  count} coals left. ({currentRow}, {currentCol})");
        }
        private static void MoveMiner(string[] commands)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                string command = commands[i];
                if (command == "up")
                {
                    Move(-1,0);
                }
                else if (command == "down")
                {
                    Move(1,0);
                }
                else if (command == "left")
                {
                    Move(0, -1);
                }
                else if (command == "right")
                {
                    Move(0, 1);
                }
            }
        }
        private static void Move(int row, int col)
        {            
            currentRow += row;
            currentCol += col;
            if (IsInside(currentRow,currentCol))
            {
                if (matrix[currentRow][currentCol] == 'c')
                {
                    matrix[currentRow][currentCol] = '*';
                    count++;
                    if (count == coalsCount)
                    {
                        IsCollect = true;
                        return;
                    }
                }
                else if (matrix[currentRow][currentCol] == 'e')
                {
                    IsDead = true;
                    return;
                }
            }
            else
            {
                currentRow -= row;
                currentCol -= col;
            }
        }
        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }
        private static void FillMatrix()
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                char[] chArr = Console.ReadLine().Split(" ").Select(char.Parse).ToArray();
                matrix[row] = new char[matrix.Length];
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    matrix[row][col] = chArr[col];
                    if (matrix[row][col] == 's')
                    {
                        currentRow = row;
                        currentCol = col;
                    }
                    else if (matrix[row][col] == 'c')
                    {
                        coalsCount++;
                    }
                }
            }
        }
    }
}
