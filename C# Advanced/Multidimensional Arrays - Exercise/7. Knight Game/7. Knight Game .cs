using System;

namespace _7._Knight_Game
{
    class Program
    {
        static char[][] matrix;
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());
            matrix = new char[size][];

            FillMatrix(size);

            int knightsCountToRemove = 0;
            int targetAttack = 0;
            int targetRow = 0;
            int targetCol = 0;

            do
            {
                if (targetAttack > 0)
                {
                    matrix[targetRow][targetCol] = '0';
                    targetAttack = 0;
                    knightsCountToRemove++;
                }
                int currentAttack = 0;

                for (int row = 0; row < matrix.Length; row++)
                {
                    for (int col = 0; col < matrix[row].Length; col++)
                    {
                        if (matrix[row][col] == 'K')
                        {
                            currentAttack = CalculateAttack(row,col);
                            if (currentAttack > targetAttack)
                            {
                                targetAttack = currentAttack;
                                targetRow = row;
                                targetCol = col;
                            }
                        }
                    }
                }
            } while (targetAttack > 0);
            Console.WriteLine(knightsCountToRemove);
        }

        private static int CalculateAttack(int row, int col)
        {
            int result = 0;
            if (IspositionAttacked(row - 1,col - 2 )) result++;
            if (IspositionAttacked(row + 1,col - 2)) result++;
            if (IspositionAttacked(row - 1,col + 2)) result++;
            if (IspositionAttacked(row + 1,col + 2)) result++;
            if (IspositionAttacked(row - 2,col - 1)) result++;
            if (IspositionAttacked(row - 2,col + 1)) result++;
            if (IspositionAttacked(row + 2,col - 1)) result++;
            if (IspositionAttacked(row + 2,col + 1)) result++;

            return result;
        }

        private static bool IspositionAttacked(int row, int col)
        {
            return IsOnChessBoard(row,col) && matrix[row][col] == 'K';
        }

        private static bool IsOnChessBoard(int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }
        private static void FillMatrix(int size)
        {
            for (int row = 0; row < matrix.Length; row++)
            {
                matrix[row] = Console.ReadLine().ToCharArray();
            }
        }
    }
}
