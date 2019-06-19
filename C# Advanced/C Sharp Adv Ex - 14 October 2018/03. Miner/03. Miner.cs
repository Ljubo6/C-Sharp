using System;
using System.Collections.Generic;
using System.Linq;

namespace _03._Miner
{
    class Program
    {
        static string[][] jaggedMatrix;
        static int rowMiner;
        static int colMiner;
        static int coalCount;
        static bool isDead;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            jaggedMatrix = new string[n][];
            string[] commands = Console.ReadLine().Split();
            Queue<string> orders = new Queue<string>(commands);

            FillMatrix();
            coalCount = CalculateCoals();

            while (true)
            {
                string order = orders.Dequeue();
                switch (order)
                {
                    case "up":
                        if (rowMiner == 0)
                        {
                            continue;
                        }
                        else
                        {
                            rowMiner--;
                            MoveMiner();
                        }
                        break;
                    case "down":
                        if (rowMiner == jaggedMatrix.Length - 1)
                        {
                            continue;
                        }
                        else
                        {
                            rowMiner++;
                            MoveMiner();
                        }
                        break;
                    case "left":
                        if (colMiner == 0)
                        {
                            continue;
                        }
                        else
                        {
                            colMiner--;
                            MoveMiner();
                        }
                        break;
                    case "right":
                        if (colMiner == jaggedMatrix[rowMiner].Length - 1)
                        {
                            continue;
                        }
                        else
                        {
                            colMiner++;
                            MoveMiner();
                        }
                        break;
                    default:
                        break;
                }
                if (coalCount == 0)
                {
                    Console.WriteLine($"You collected all coals! ({rowMiner}, {colMiner})");
                    break;
                }
                else if (isDead)
                {
                    Console.WriteLine($"Game over! ({rowMiner}, {colMiner})");
                    break;
                }
                else if (!orders.Any())
                {
                    Console.WriteLine($"{coalCount} coals left. ({rowMiner}, {colMiner})");
                    break;
                }

            }
        }

        private static void MoveMiner()
        {
            string nextposition = jaggedMatrix[rowMiner][colMiner];
            if (nextposition == "c")
            {
                jaggedMatrix[rowMiner][colMiner] = "*";
                coalCount--;
            }
            else if (nextposition == "e")
            {
                isDead = true;
            }
        }

        private static int CalculateCoals()
        {
            int count = 0;
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                for (int col = 0; col < jaggedMatrix[row].Length; col++)
                {
                    if (jaggedMatrix[row][col] == "c")
                    {
                        count++;
                    }
                    if (jaggedMatrix[row][col] == "s")
                    {
                        rowMiner = row;
                        colMiner = col;
                    }
                }
            }
            return count;
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ", jaggedMatrix[row])); 
            }
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split();
            }
        }
    }
}
