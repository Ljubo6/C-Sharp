using System;
using System.Linq;

namespace _03._Space_Station_Establishment
{
    class Program
    {
        static char[][] jaggedArray;
        static int targetRow;
        static int targetCol;
        static int rowBlackHole;
        static int colBlackHole;
        static int collectEnergy;
        static bool isDisappear;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            collectEnergy = 0;
            isDisappear = false;
            jaggedArray = new char[n][];

            FillMatrix();

            while (true)
            {               
                if (collectEnergy >= 50 || isDisappear == true)
                {
                    break;
                }

                string command = Console.ReadLine();

                switch (command)
                {
                    case "up":
                        if (IsInSpace(targetRow - 1, targetCol))
                        {
                            jaggedArray[targetRow][targetCol] = '-';
                            MovePlayer(--targetRow, targetCol);
                        }
                        else
                        {
                            isDisappear = true;
                            jaggedArray[targetRow][targetCol] = '-';
                        }                        
                        break;
                    case "down":
                        if (IsInSpace(targetRow + 1, targetCol))
                        {
                            jaggedArray[targetRow][targetCol] = '-';
                            MovePlayer(++targetRow, targetCol);
                        }
                        else
                        {
                            isDisappear = true;
                            jaggedArray[targetRow][targetCol] = '-';
                        }
                        break;
                    case "left":
                        if (IsInSpace(targetRow, targetCol - 1))
                        {
                            jaggedArray[targetRow][targetCol] = '-';
                            MovePlayer(targetRow, --targetCol);
                        }
                        else
                        {
                            isDisappear = true;
                            jaggedArray[targetRow][targetCol] = '-';
                        }
                        break;
                    case "right":
                        if (IsInSpace(targetRow, targetCol + 1))
                        {
                            jaggedArray[targetRow][targetCol] = '-';
                            MovePlayer(targetRow, ++targetCol);
                        }
                        else
                        {
                            isDisappear = true;
                            jaggedArray[targetRow][targetCol] = '-';
                        }
                        break;
                    default:
                        break;
                }
            }
            if (isDisappear == true)
            {
                Console.WriteLine("Bad news, the spaceship went to the void.");
            }
            else if (isDisappear == false && collectEnergy >= 50)
            {
                Console.WriteLine("Good news! Stephen succeeded in collecting enough star power!");
            }
            Console.WriteLine($"Star power collected: {collectEnergy}");
            PrintMatrix();
        }

        private static void MovePlayer(int row, int col)
        {
            if (char.IsDigit(jaggedArray[row][col]))
            {
                collectEnergy += int.Parse(jaggedArray[row][col].ToString());
                jaggedArray[row][col] = 'S';
            }
            else if (char.IsLetter(jaggedArray[row][col]))
            {
                jaggedArray[row][col] = 'S';
                FindBlackHole();
                jaggedArray[row][col] = '-';
                targetRow = rowBlackHole;
                targetCol = colBlackHole;
                jaggedArray[targetRow][targetCol] = 'S';
            }
        }

        private static bool IsInSpace(int row, int col)
        {
            return row >= 0 && row < jaggedArray.Length && col >= 0 && col < jaggedArray[row].Length;
        }

        private static void FindBlackHole()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                for (int col = 0; col < jaggedArray[row].Length; col++)
                {
                    if (jaggedArray[row][col] == 'O')
                    {
                        rowBlackHole = row;
                        colBlackHole = col;
                    }
                }
            }
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                Console.WriteLine(string.Join("",jaggedArray[row]));
            }
        }

        private static void FillMatrix()
        {
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                jaggedArray[row] = Console.ReadLine().ToCharArray();
                if (jaggedArray[row].Contains('S'))
                {
                    targetRow = row;
                    targetCol = Array.IndexOf(jaggedArray[row],'S');
                }
            }
        }
    }
}
