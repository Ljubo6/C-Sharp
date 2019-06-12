using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _01._Club_Party
{
    class Program
    {
        private static void Main()
        {
            int maxCapacity = int.Parse(Console.ReadLine());
            string[] input = Console.ReadLine().Split();
            List<string> letters = new List<string>();
            List<int> digits = new List<int>();
            bool isFillRoomForFirstTime = false;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                string element = input[i];
                if (char.IsLetter(element[0]))
                {
                    letters.Add(element);
                }
                else if (char.IsDigit(element[0]))
                {

                    if (letters.Count == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (isFillRoomForFirstTime == false)
                        {
                            digits.Add(int.Parse(element));
                            isFillRoomForFirstTime = true;
                        }
                        else
                        {
                            if (digits.Sum() + int.Parse(element) <= maxCapacity)
                            {
                                if (digits.Sum() + int.Parse(element) == maxCapacity)
                                {
                                    digits.Add(int.Parse(element));
                                    Print(digits, letters[0]);
                                    letters.RemoveAt(0);
                                    digits.Clear();
                                }
                                else
                                {
                                    digits.Add(int.Parse(element));
                                }
                            }
                            else
                            {
                                Print(digits, letters[0]);
                                letters.RemoveAt(0);
                                digits.Clear();

                                if (int.Parse(element) <= maxCapacity && letters.Any())
                                {
                                    digits.Add(int.Parse(element));
                                }
                            }
                        }
                    }                  
                }
            }
        }

        private static void Print(List<int> digits, string letter)
        {
            Console.WriteLine($"{letter} -> {string.Join(", ", digits)}");
        }
    }
}
