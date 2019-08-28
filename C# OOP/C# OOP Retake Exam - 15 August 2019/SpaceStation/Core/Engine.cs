using SpaceStation.Core.Contracts;
using SpaceStation.IO;
using SpaceStation.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Engine : IEngine
    {
        private IWriter writer;
        private IReader reader;
        private IController controller;

        public Engine()
        {
            this.writer = new Writer();
            this.reader = new Reader();
            this.controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "AddAstronaut")
                    {
                        Console.WriteLine(controller.AddAstronaut(input[1],input[2]));
                    }
                    else if (input[0] == "AddPlanet")
                    {
                        string[] param = input.Skip(2).ToArray();
                        if (param.Length > 0)
                        {
                            Console.WriteLine(controller.AddPlanet(input[1], param));
                        }
                        else
                        {
                            Console.WriteLine(controller.AddPlanet(input[1]));
                        }
                        
                    }
                    else if (input[0] == "RetireAstronaut")
                    {
                        Console.WriteLine(controller.RetireAstronaut(input[1]));
                    }
                    else if (input[0] == "ExplorePlanet")
                    {
                        Console.WriteLine(controller.ExplorePlanet(input[1]));
                    }
                    else if(input[0] == "Report")
                    {
                        Console.WriteLine(controller.Report());
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
