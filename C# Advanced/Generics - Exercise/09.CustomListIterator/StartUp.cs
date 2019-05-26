using System;
using System.Collections.Generic;

namespace _08.CustomListSorter
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            ISoftuniList<string> softuniList = new SoftuniList<string>();

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "END")
            {
                string element = string.Empty;
                string[] inputArgs = input.Split();

                string command = inputArgs[0];

                switch (command)
                {
                    case "Add":
                        element = inputArgs[1];
                        softuniList.Add(element);
                        break;
                    case "Remove":
                        int index = int.Parse(inputArgs[1]);
                        softuniList.Remove(index);
                        break;
                    case "Contains":
                        element = inputArgs[1];
                        Console.WriteLine(softuniList.Contains(element));
                        break;
                    case "Swap": 
                        int index1 = int.Parse(inputArgs[1]);
                        int index2 = int.Parse(inputArgs[2]);
                        softuniList.Swap(index1,index2);
                        break;
                    case "Greater":
                        element = inputArgs[1];
                        Console.WriteLine(softuniList.CountGreaterThan(element));
                        break;
                    case "Max":
                        Console.WriteLine(softuniList.Max());
                        break;
                    case "Min":
                        Console.WriteLine(softuniList.Min());
                        break;
                    case "Sort":
                        softuniList.Sort();
                        break;
                    case "Print":
                        Console.WriteLine(softuniList);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
