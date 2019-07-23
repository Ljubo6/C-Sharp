namespace Solid.Logger.Core
{
    using System;

    using Solid.Logger.Core.Contracts;
    public class Engine : IEngine
    {
        private ICommandInterpreter commandInterpreter;
        public Engine(ICommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }
        public void Run()
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split();

                this.commandInterpreter.AddAppender(inputArgs);
            }

            string input = string.Empty; ;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] inputArgs = input.Split('|');
                this.commandInterpreter.AddMessage(inputArgs);
            }

            this.commandInterpreter.PrintInfo();
        }
    }
}
