using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _02._Activation_Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split('&');
            string pattern = @"^[A-Za-z0-9]+$";
            Regex regex = new Regex(pattern);
            List<string> strList = new List<string>();
            foreach (var element in input)
            {
                if (regex.IsMatch(element))
                {
                   
                    char[] currentString = element.ToCharArray();
                    for (int i = 0; i < element.Length; i++)
                    {
                        if (char.IsDigit(element[i]))
                        {
                            int num = int.Parse(element[i].ToString());
                            int n = 9 - num;
                            string ch = n.ToString() ;
                            currentString[i] = ch[0];
                            
                        }
                    }
                    string newStr = new string(currentString).ToUpper();
                    List<string> currentList = new List<string>();
                    if (currentString.Length == 16)
                    {
                        for (int i = 0; i < newStr.Length; i += 4)
                        {
                            string segment = newStr.Substring(i,4);
                            currentList.Add(segment);
                        }
                    }
                    else if (currentString.Length == 25)
                    {
                        for (int i = 0; i < newStr.Length; i += 5)
                        {
                            string segment = newStr.Substring(i, 5);
                            currentList.Add(segment);
                        }
                    }
                    string str = String.Join("-",currentList);
                    strList.Add(str);
                }
            }
            Console.WriteLine(string.Join(", ",strList));
        }
    }
}
