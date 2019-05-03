using System;

namespace _4._Symbol_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[][] jaggedMatrix = new char[n][];
            InputMatrix(jaggedMatrix);
            char symbol = char.Parse(Console.ReadLine());
            CheckSymbol(jaggedMatrix, symbol);

        }

        private static void CheckSymbol(char[][] jaggedMatrix, char symbol)
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                for (int col = 0; col < jaggedMatrix[row].Length; col++)
                {
                    if (jaggedMatrix[row][col] == symbol)
                    {
                        Console.WriteLine($"({row}, {col})");
                        return;
                    }
                }
            }
            Console.WriteLine($"{symbol} does not occur in the matrix ");
        }

        private static void InputMatrix(char[][] jaggedMatrix)
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().ToCharArray();
            }
        }
    }
}
