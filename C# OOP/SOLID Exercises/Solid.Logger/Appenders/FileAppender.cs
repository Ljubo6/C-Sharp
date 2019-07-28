namespace Solid.Logger.Appenders
{
    using System;

    using Contracts;
    using Solid.Logger.Layouts.Contracts;
    using Solid.Logger.Loggers.Contracts;
    using Solid.Logger.Loggers.Enums;
    using System.IO;

    public class FileAppender : Appender
    {
        private const string path = @"..\..\..\log.txt";

        private readonly ILogFile logFile;

        public FileAppender(ILayout layout,ILogFile logfile)
            :base(layout)

        {
            this.logFile = logfile;
        }

        public override void Append(string dateTime, ReportLevel reportLevel, string message)
        {
            if (reportLevel >= this.ReportLevel)
            {
                this.MessagesCount++;
                string content = string.Format(this.Layout.Format, dateTime, reportLevel, message) + Environment.NewLine;
                this.logFile.Write(content);
                File.AppendAllText(path, content);
            }
        }
        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {this.Layout.GetType().Name}, " +
                $"Report level: {this.ReportLevel.ToString().ToUpper()}, Messages appended: {this.MessagesCount}, " +
                $"File size: {this.logFile.Size}";
        }
    }
}
