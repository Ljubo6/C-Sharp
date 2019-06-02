using System;
using System.IO;

namespace _6.FolderSize
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] files = Directory.GetFiles("TestFolder");
            double sum = 0;
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                sum += fileInfo.Length;
            }
            sum = sum / 1024 / 1024;
            File.WriteAllText("output.txt",sum.ToString());
        }
    }
}
