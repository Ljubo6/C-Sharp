using System;
using System.Collections.Generic;
using System.Linq;

namespace _02._Excel_Functions
{
    class Program
    {
        static string[][] jaggedMatrix;
        static int indexTargetHeader;
        static int n;
        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());

            jaggedMatrix = new string[n][];

            FillMatrix();

            string[] command = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            string order = command[0];
            string header = command[1];

            switch (order)
            {
                case "hide":DeleteCol(header);
                    break;
                case "sort":SortByHeader(header);
                    break;
                case "filter":
                    string value = command[2];
                    FilterMatrix(header, value);
                    break;
                default:
                    break;
            }
        }
        private static void FilterMatrix(string header, string value)
        {
            indexTargetHeader = Array.IndexOf(jaggedMatrix[0], header);
            Console.WriteLine(string.Join(" | ", jaggedMatrix[0]));
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                if (jaggedMatrix[row][indexTargetHeader] == value)
                {
                    Console.WriteLine(string.Join(" | ", jaggedMatrix[row]));
                }
            }
        }
        private static void SortByHeader(string header)
        {
            indexTargetHeader = Array.IndexOf(jaggedMatrix[0], header);
            List<string> arr = new List<string>();
            for (int row = 1; row < jaggedMatrix.Length; row++)
            {
                arr.Add(jaggedMatrix[row][indexTargetHeader]);
                arr = arr.OrderBy(x => x).ToList();
            }
            Console.WriteLine(string.Join(" | ", jaggedMatrix[0]));
            for (int i = 0; i < arr.Count; i++)
            {
                for (int row = 1; row < jaggedMatrix.Length; row++)
                {
                    if (jaggedMatrix[row][indexTargetHeader] == arr[i])
                    {
                        Console.WriteLine(string.Join(" | ", jaggedMatrix[row]));
                    }                   
                }
            }
        }
        private static void DeleteCol(string header)
        {
            indexTargetHeader = Array.IndexOf(jaggedMatrix[0],header);
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                for (int col = 0; col < jaggedMatrix[row].Length; col++)
                {
                    if (col == indexTargetHeader)
                    {
                        jaggedMatrix[row][col] = null;
                    }
                }
                
            }
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                for (int col = indexTargetHeader; col < jaggedMatrix[row].Length - 1; col++)
                {
                    if (indexTargetHeader < jaggedMatrix[row].Length - 1)
                    {
                        jaggedMatrix[row][col] = jaggedMatrix[row][col + 1];
                        
                    }                   
                }
            }
            int size = jaggedMatrix[0].Length - 1;
            for (int i = 0; i < jaggedMatrix.Length; i++)
            {
                Array.Resize( ref jaggedMatrix[i], size);
            }
            PrintJaggedMatrix();
        }
        private static void PrintJaggedMatrix()
        {
            for (int i = 0; i < jaggedMatrix.Length; i++)
            {
                Console.WriteLine(string.Join(" | ",jaggedMatrix[i]));
            }
        }
        private static void FillMatrix()
        {
            for (int i = 0; i < jaggedMatrix.Length; i++)
            {
                jaggedMatrix[i] = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            }
        }
    }
}
