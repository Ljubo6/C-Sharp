using System;
using System.Linq;

namespace _6._Jagged_Array_Modification
{
    class Program
    {
        static void Main(string[] args)
        {
            int jaggedMatrixSize = int.Parse(Console.ReadLine());

            int[][] jaggedMatrix = new int[jaggedMatrixSize][];

            FillMatrix(jaggedMatrix);

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split();
                string command = tokens[0];
                int targetRow = int.Parse(tokens[1]);
                int targetCol = int.Parse(tokens[2]);
                int number = int.Parse(tokens[3]);

                if (!IsValidCoordinate(targetRow, targetCol, jaggedMatrix))
                {
                    Console.WriteLine("Invalid coordinates");
                }
                else
                {
                    if (command == "Add")
                    {
                        jaggedMatrix[targetRow][targetCol] += number;
                    }
                    else
                    {
                        jaggedMatrix[targetRow][targetCol] -= number;
                    }

                }

            }
            PrintMatrix(jaggedMatrix);
        }

        private static void PrintMatrix(int[][] jaggedMatrix)
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ", jaggedMatrix[row]));
            }
        }

        private static bool IsValidCoordinate(int targetRow, int targetCol, int[][] jaggedMatrix)
        {
            return targetRow >= 0 && targetRow < jaggedMatrix.Length && targetCol >= 0 && targetCol < jaggedMatrix[targetRow].Length;
        }

        private static void FillMatrix(int[][] jaggedMatrix)
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split().Select(int.Parse).ToArray();
            }
        }
    }
}
