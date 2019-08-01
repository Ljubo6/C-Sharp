using SoftUniRestaurant.Core.Factories;
using SoftUniRestaurant.Core.Factories.Contracts;

namespace SoftUniRestaurant
{
    public class StartUp
    {
        public static void Main()
        {
            Engine engine = new Engine();
            engine.Run();
        }
    }
}
