using System;

namespace _02._Knight_Game
{
    class Program
    {       
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            char[][] jaggedArray = new char[n][];
            FillMatrix(jaggedArray);
            int targetRow = 0;
            int targetCol = 0;
            int removedKnights = 0;
            while (true)
            {
                int maxAttack = 0;
                

                for (int row = 0; row < jaggedArray.Length; row++)
                {
                    for (int col = 0; col < jaggedArray[row].Length; col++)
                    {
                        int attacked = 0;
                        if (jaggedArray[row][col] == 'K')
                        {
                            if (IsInside(jaggedArray,row - 2, col - 1) && jaggedArray[row - 2][col - 1] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row - 2, col + 1) && jaggedArray[row - 2][col + 1] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row + 2, col - 1) && jaggedArray[row + 2][col - 1] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row + 2, col + 1) && jaggedArray[row + 2][col + 1] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row - 1, col - 2) && jaggedArray[row - 1][col - 2] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row + 1, col - 2) && jaggedArray[row + 1][col - 2] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row - 1, col + 2) && jaggedArray[row - 1][col + 2] == 'K')
                            {
                                attacked++;
                            }
                            if (IsInside(jaggedArray, row + 1, col + 2) && jaggedArray[row + 1][col + 2] == 'K')
                            {
                                attacked++;
                            }
                        }
                        
                        if (attacked > maxAttack)
                        {
                            maxAttack = attacked;
                            targetRow = row;
                            targetCol = col;
                        }
                    }

                }
                if (maxAttack > 0)
                {
                    jaggedArray[targetRow][targetCol] = '0';
                    removedKnights++;
                }
                else
                {
                    Console.WriteLine(removedKnights);
                    break;
                }

            }

        }

        private static bool IsInside(char[][] jaggedArray, int row, int col)
        {
            return row >= 0 && row < jaggedArray.Length && col >= 0 && col < jaggedArray[row].Length;
        }

        private static void FillMatrix(char[][] jaggedArray)
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                jaggedArray[row] = Console.ReadLine().ToCharArray();
            }
        }
    }
}
