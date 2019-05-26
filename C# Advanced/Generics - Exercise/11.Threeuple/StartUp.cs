namespace _11.Threeuple
{
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] personInfo = Console.ReadLine().Split();
            string fullName = personInfo[0] + " " + personInfo[1];
            string neighborhood = personInfo[2];
            string town = personInfo[3];

            string[] beerInfo = Console.ReadLine().Split();
            string name = beerInfo[0];
            int liters = int.Parse(beerInfo[1]);
            bool isDrunk = beerInfo[2] == "drunk" ? true : false;

            string[] sashoInfo = Console.ReadLine().Split();
            string sashoName = sashoInfo[0];
            double sashoDouble = double.Parse(sashoInfo[1]);
            string sashoBanka = sashoInfo[2];

            SpecialTuple<string, string,string> personTuple = new SpecialTuple<string, string,string>(fullName,neighborhood, town);
            SpecialTuple<string, int,bool> beerTuple = new SpecialTuple<string, int,bool>(name, liters,isDrunk);
            SpecialTuple<string, double,string> specialTuple = new SpecialTuple<string, double,string>(sashoName, sashoDouble,sashoBanka);

            Console.WriteLine(personTuple);
            Console.WriteLine(beerTuple);
            Console.WriteLine(specialTuple);
        }
    }
}
