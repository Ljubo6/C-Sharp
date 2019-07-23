

namespace Solid.Logger.Loggers
{
    using System.Linq;

    using Solid.Logger.Loggers.Contracts;
    public class LogFile : ILogFile
    {
        public int Size { get; private set; }

        public void Write(string message)
        {
            this.Size += message.Where(char.IsLetter).Sum(x => x);
        }
    }
}
