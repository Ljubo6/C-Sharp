using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace _03._Santa_s_Secret_Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            int key = int.Parse(Console.ReadLine());
            string input = string.Empty;
            List<string> validStr = new List<string>();
            List<string> childGood = new List<string>();
            while ((input = Console.ReadLine()) != "end")
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < input.Length; i++)
                {
                    char ch = (char)(input[i] - key);
                    sb.Append(ch);

                }
                string strToList = sb.ToString();
                string pattern = @"([^@:!\->]*)@[A-Za-z]+([^@:!\->]*)!([GN])!([^@:!\->]*)";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(strToList))
                {
                    validStr.Add(strToList);
                }
                
            }          
            string patternName = @"(?<=\@)([A-Za-z]+)";
            string patternBehavior = @"(?<=\!)[GN](?=\!)";
            Regex nameRegex = new Regex(patternName);
            Regex behavierRegex = new Regex(patternBehavior);

            for (int i = 0; i < validStr.Count; i++)
            {
                string name = GetMatch(nameRegex,validStr[i]);
                string behavier = GetMatch(behavierRegex,validStr[i]);
                if (behavier == "G")
                {
                    childGood.Add(name);
                }
            }
            Console.WriteLine(string.Join(Environment.NewLine, childGood));
        }

        private static string GetMatch(Regex reg, string str)
        {
            string validString = reg.Match(str).ToString();
            return validString;
        }
    }
}
