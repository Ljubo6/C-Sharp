using System;
using System.Collections.Generic;
using System.IO;

namespace _4.MergeFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string textOne = "FileOne.txt";
            string textTwo = "FileTwo.txt";
            string[] textLineOne = File.ReadAllLines(textOne);
            string[] textLineTwo = File.ReadAllLines(textTwo);
            int count = 0;
            List<string> mergedList = new List<string>();
            if ((textLineOne.Length - textLineTwo.Length) > 0)
            {
                count = textLineTwo.Length;
                mergedList = Merge(count,textLineOne,textLineTwo,1);
            }
            else if ((textLineOne.Length - textLineTwo.Length) < 0)
            {
                count = textLineOne.Length;
                mergedList = Merge(count, textLineOne, textLineTwo, 2);
            }
            else
            {
                count = textLineOne.Length;
                mergedList = Merge(count, textLineOne, textLineTwo, 0);
            }
            string outputResultPath = "output.txt";

            for (int i = 0; i < mergedList.Count; i++)
            {
                File.AppendAllText(outputResultPath, $"{mergedList[i]}{Environment.NewLine}");
            }

        }

        private static List<string> Merge(int count, string[] textLineOne, string[] textLineTwo, int v)
        {
            List<string> mergedList = new List<string>();
            int startIndex = Math.Abs(textLineOne.Length - textLineTwo.Length);
            for (int i = 0; i < count; i++)
            {
                mergedList.Add(textLineOne[i]);
                mergedList.Add(textLineTwo[i]);
            }
            if (v == 1)
            {
                for (int i = startIndex; i < textLineOne.Length; i++)
                {
                    mergedList.Add(textLineOne[i]);
                }
            }
            else if (v == 2)
            {
                for (int i = startIndex; i < textLineTwo.Length; i++)
                {
                    mergedList.Add(textLineTwo[i]);
                }
            }
            return mergedList;
        }
    }
}
