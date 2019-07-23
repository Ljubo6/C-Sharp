namespace Solid.Logger
{
    using Solid.Logger.Core.Contracts;
    using Solid.Logger.Core;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter(); 
            IEngine engine = new Engine(commandInterpreter);
            engine.Run();
        }
    }
}
