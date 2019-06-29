using System;
using System.Collections.Generic;
using System.Linq;

namespace P05_GreedyTimes
{

    public class Potato
    {
        static Bag bag;
        static void Main(string[] args)
        {
            long bagCapacity = long.Parse(Console.ReadLine());
            string[] seif = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            bag = new Bag();
            bag.FillBag(seif,bagCapacity);

            Console.WriteLine(bag);          
        }
    }
}