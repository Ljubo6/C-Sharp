namespace Solid.Logger.Loggers
{
    using Contracts;
    using Appenders.Contracts;
    using Solid.Logger.Loggers.Enums;

    public class Logger : ILogger
    {
        private readonly IAppender consoleAppender;
        private readonly IAppender fileAppender;

        public Logger(IAppender consoleAppender)
        {
            this.consoleAppender = consoleAppender;
        }
        public Logger(IAppender consoleAppender, IAppender fileAppender)
            :this(consoleAppender)
        {
            this.fileAppender = fileAppender;
        }
        public void Error(string dateTime, string errorMessage)
        {
            this.AppendMessage(dateTime,ReportLevel.ERROR,errorMessage);            
        }

        public void Warning(string dateTime, string warningMessage)
        {
            this.AppendMessage(dateTime,ReportLevel.WARNING,warningMessage);
        }

        public void Critical(string dateTime, string criticalMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.CRITICAL, criticalMessage);
        }

        public void Fatal(string dateTime, string fatalMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.FATAL, fatalMessage);
        }

        public void Info(string dateTime, string infoMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.INFO, infoMessage);
        }
        private void AppendMessage(string dateTime, ReportLevel reportLevel, string message)
        {

            this.fileAppender?.Append(dateTime, reportLevel, message);
            this.consoleAppender?.Append(dateTime, reportLevel, message);
        }
    }
}
