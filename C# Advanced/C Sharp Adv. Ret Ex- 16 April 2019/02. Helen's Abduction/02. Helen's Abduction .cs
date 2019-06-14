using System;
using System.Linq;

namespace _02._Helen_s_Abduction
{
    class Program
    {
        static char[][] jaggedMatrix;
        static int parisTargetRow;
        static int parisTargetCol;
        static int parisEnergy;
        static void Main(string[] args)
        {
            parisEnergy = int.Parse(Console.ReadLine());
            int matrixSize = int.Parse(Console.ReadLine());
            jaggedMatrix = new char[matrixSize][];

            FillMatrix();

            while (true)
            {
                string[] input = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string parisDirection = input[0];
                int spartanRow = int.Parse(input[1]);
                int spartanCol = int.Parse(input[2]);
                jaggedMatrix[spartanRow][spartanCol] = 'S';
                switch (parisDirection)
                {
                    case "up":
                        if (parisTargetRow == 0)
                        {
                            parisEnergy--;
                        }
                        else
                        {
                            jaggedMatrix[parisTargetRow][parisTargetCol] = '-';
                            parisEnergy--;
                            parisTargetRow--;
                            MoveParis();
                        }
                        break;
                    case "down":
                        if (parisTargetRow == matrixSize - 1)
                        {
                            parisEnergy--;
                        }
                        else
                        {
                            jaggedMatrix[parisTargetRow][parisTargetCol] = '-';
                            parisEnergy--;
                            parisTargetRow++;
                            MoveParis();
                        }
                        break;
                    case "left":
                        if (parisTargetCol == 0)
                        {
                            parisEnergy--;
                        }
                        else
                        {
                            jaggedMatrix[parisTargetRow][parisTargetCol] = '-';
                            parisEnergy--;
                            parisTargetCol--;
                            MoveParis();
                        }
                        break;
                    case "right":
                        if (parisTargetCol == matrixSize - 1)
                        {
                            parisEnergy--;
                        }
                        else
                        {
                            jaggedMatrix[parisTargetRow][parisTargetCol] = '-';
                            parisEnergy--;
                            parisTargetCol++;
                            MoveParis();
                        }
                        break;
                    default:
                        break;
                }
                if (jaggedMatrix[parisTargetRow][parisTargetCol] == 'X')
                {
                    Console.WriteLine($"Paris died at {parisTargetRow};{parisTargetCol}.");
                    break;
                }
                else if (parisEnergy <= 0)
                {
                    Console.WriteLine($"Paris died at {parisTargetRow};{parisTargetCol}.");
                    break;
                }
            }
            PrintMatrix();
        }

        private static void MoveParis()
        {
            if (jaggedMatrix[parisTargetRow][parisTargetCol] == 'S')
            {
                parisEnergy -= 2;
                if (parisEnergy > 0)
                {
                    jaggedMatrix[parisTargetRow][parisTargetCol] = 'P';
                }
                else
                {
                    jaggedMatrix[parisTargetRow][parisTargetCol] = 'X';
                }
            }
            else if (jaggedMatrix[parisTargetRow][parisTargetCol] == 'H')
            {
                jaggedMatrix[parisTargetRow][parisTargetCol] = '-';
                Console.WriteLine($"Paris has successfully abducted Helen! Energy left: {parisEnergy}");
                PrintMatrix();
                Environment.Exit(0);
            }
            else
            {
                if (parisEnergy > 0)
                {
                    jaggedMatrix[parisTargetRow][parisTargetCol] = 'P';
                }
                else
                {
                    jaggedMatrix[parisTargetRow][parisTargetCol] = 'X';
                }

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
                if (jaggedMatrix[i].Contains('P'))
                {
                    parisTargetRow = i;
                    parisTargetCol = Array.IndexOf(jaggedMatrix[i], 'P');
                }
            }
        }
    }
}
