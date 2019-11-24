using System;

namespace _01.Prototype
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var sandwichMenu = new SandwichMenu();

            // Initialize with default sandwiches
            sandwichMenu["BLT"] = new Sandwich("Wheat", "Becon", "", "Tomato");
            sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly");
            sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

            //Deli manager adds custom sandiches

            sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Becon", "American","Lettuce, Tomato, Onion, Olives");
            sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey, Ham, Salami", "Provolone", "Lettuce, Onion");
            sandwichMenu["Vegeterian"] = new Sandwich("Wheat", "", "", "Lettuce, Onion, Tomato, Olives, Spinach");

            // Now we can clone these sandwiches
            var sandwich1 = sandwichMenu["BLT"].Clone() as Sandwich;
            var sandwich2 = sandwichMenu["ThreeMeatCombo"].Clone() as Sandwich;
            var sandwich3 = sandwichMenu["Vegeterian"].Clone() as Sandwich;
        }
    }
}
