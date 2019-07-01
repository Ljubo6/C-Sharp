namespace OnlineRadioDatabase
{
    using OnlineRadioDatabase.Core;
    using System;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Engine engine = new Engine();
            engine.Run();
        }
    }
}
