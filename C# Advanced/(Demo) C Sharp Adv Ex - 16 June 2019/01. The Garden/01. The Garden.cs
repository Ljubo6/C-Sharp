using System;
using System.Linq;

namespace _01._The_Garden
{
    class Program
    {
        static string[][] jaggedMatrix;
        static int rHarvest;
        static int cHarvest;
        static int rMole;
        static int cMole;
        static int carrotCount;
        static int potatoCount;
        static int lettuceCount;
        static int harmedVegetable;
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            jaggedMatrix = new string[n][];

            Fillmatrix();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "End of Harvest")
            {
                string[] tokens = input.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                if (command == "Harvest")
                {
                    rHarvest = int.Parse(tokens[1]);
                    cHarvest = int.Parse(tokens[2]);
                    bool isHarvestValid = IsHarvestCoordinateValid();
                    if (isHarvestValid)
                    {
                        Harvest();
                    }
                    
                }
                else if (command == "Mole")
                {
                    rMole = int.Parse(tokens[1]);
                    cMole = int.Parse(tokens[2]);
                    bool isMoleValid = IsMollCoordinatevalid();
                    string direction = tokens[3];
                    if (isMoleValid)
                    {
                        Harme(direction);
                    }                   
                }                
            }
            Printmatrix();
            Console.WriteLine($"Carrots: {carrotCount}");
            Console.WriteLine($"Potatoes: {potatoCount}");
            Console.WriteLine($"Lettuce: {lettuceCount}");
            Console.WriteLine($"Harmed vegetables: {harmedVegetable}");
        } 

        private static void Harme(string direction)
        {
            switch (direction)
            {
                case "up":
                    for (int row = rMole; row >= 0; row -= 2)
                    {
                        int col = cMole;
                        if (IsInside(row,col))
                        {
                            if (jaggedMatrix[row][cMole] != " ")
                            {
                                harmedVegetable++;
                                jaggedMatrix[row][cMole] = " ";
                            }

                        }
                       
                    }
                    break;
                case "down":
                    for (int row = rMole; row < jaggedMatrix.Length; row += 2)
                    {
                        int col = cMole;
                        if (IsInside(row,col))
                        {
                            if (jaggedMatrix[row][cMole] != " ")
                            {
                                harmedVegetable++;
                                jaggedMatrix[row][cMole] = " ";
                            }
                        }

                        
                    }
                    break;
                case "left":
                    for (int col = cMole; col >= 0; col -= 2)
                    {
                        int row = rMole;
                        if (IsInside(row, col))
                        {
                            if (jaggedMatrix[rMole][col] != " ")
                            {
                                harmedVegetable++;
                                jaggedMatrix[rMole][col] = " ";
                            }
                        }
                    }
                    break;
                case "right":
                    for (int col = cMole; col < jaggedMatrix[rMole].Length; col += 2)
                    {
                        int row = rMole;
                        if (IsInside(row, col))
                        {
                            if (jaggedMatrix[rMole][col] != " ")
                            {
                                harmedVegetable++;
                                jaggedMatrix[rMole][col] = " ";
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private static bool IsInside(int row, int col)
        {
            return row >= 0 && row < jaggedMatrix.Length
                && col >= 0 && col < jaggedMatrix[row].Length;
        }
        private static bool IsMollCoordinatevalid()
        {
            return rMole >= 0 && rMole < jaggedMatrix.Length
                && cMole >= 0 && cMole < jaggedMatrix[rMole].Length;
        }

        private static bool IsHarvestCoordinateValid()
        {
            return rHarvest >= 0 && rHarvest < jaggedMatrix.Length 
                && cHarvest >= 0 && cHarvest < jaggedMatrix[rHarvest].Length;
        }
        private static void Harvest()
        {
            if (jaggedMatrix[rHarvest][cHarvest] == "C")
            {
                carrotCount++;
                jaggedMatrix[rHarvest][cHarvest] = " ";
            }
            else if (jaggedMatrix[rHarvest][cHarvest] == "P")
            {
                potatoCount++;
                jaggedMatrix[rHarvest][cHarvest] = " ";
            }
            else if (jaggedMatrix[rHarvest][cHarvest] == "L")
            {
                lettuceCount++;
                jaggedMatrix[rHarvest][cHarvest] = " ";
            }
        }
        private static void Printmatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                Console.WriteLine(string.Join(" ", jaggedMatrix[row]));
            }
        }
        private static void Fillmatrix()
        {
            for (int row = 0; row < jaggedMatrix.Length; row++)
            {
                jaggedMatrix[row] = Console.ReadLine().Split(" ",StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
