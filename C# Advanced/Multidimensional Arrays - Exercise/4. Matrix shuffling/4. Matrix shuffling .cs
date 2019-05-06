using System;
using System.Linq;

namespace _4._Matrix_shuffling
{
    class Program
    {
        static string[][] jaggedMatrix;
        static void Main(string[] args)
        {
            int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int r = size[0];
            int c = size[1];

            jaggedMatrix = new string[r][];
            FillMatrix();

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] tokens = input.Split();


                if (tokens.Length == 5)
                {
                    string command = tokens[0];
                    int rowOne = int.Parse(tokens[1]);
                    int colOne = int.Parse(tokens[2]);
                    int rowTwo = int.Parse(tokens[3]);
                    int colTwo = int.Parse(tokens[4]);
                    if (IsValid(tokens, command, rowOne, colOne, rowTwo, colTwo))
                    {

                        string current = jaggedMatrix[rowOne][colOne];
                        jaggedMatrix[rowOne][colOne] = jaggedMatrix[rowTwo][colTwo];
                        jaggedMatrix[rowTwo][colTwo] = current;

                        for (int row = 0; row < jaggedMatrix.Length; row++)
                        {
                            Console.WriteLine(string.Join(" ", jaggedMatrix[row]));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }
            }
        }

        private static bool IsValid(string[] tokens, string command, int rowOne, int colOne, int rowTwo, int colTwo)
        {
            return tokens.Length == 5 && command == "swap"
                && rowOne >= 0 && rowOne < jaggedMatrix.Length
                && colOne >= 0 && colOne < jaggedMatrix[rowOne].Length
                && rowTwo >= 0 && rowTwo < jaggedMatrix.Length
                && colTwo >= 0 && colTwo < jaggedMatrix[rowTwo].Length;
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
