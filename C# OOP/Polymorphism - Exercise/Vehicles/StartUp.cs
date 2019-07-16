using System;
using VehiclesExtension.Core;

namespace VehiclesExtension
{
    public class StartUp
    { 
        public static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.Run();
        }
    }
}
