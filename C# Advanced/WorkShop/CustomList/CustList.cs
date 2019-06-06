namespace CustomList
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            CustomListList temp = new CustomListList();
            temp.Add(1);
            temp.Add(2);
            temp.Add(3);
            temp.Add(4);
            temp.Add(5);
            temp.Add(6);
            temp.Insert(0,7);
            Console.WriteLine();
        }
    }
}
