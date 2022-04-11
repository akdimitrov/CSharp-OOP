namespace Logger.Appenders
{
    using System;
    using System.IO;
    using Layouts;
    using LogFiles;
    using ReportLevels;

    public class FileAppender : Appender
    {
        private const string Path = "../../../log.txt";
        private readonly ILogFile logFile;

        public FileAppender(ILayout layout, ILogFile logFile) : base(layout)
           => this.logFile = logFile;

        public override void Append(DateTime dateTime, ReportLevel reportLevel, string message)
        {
            string ouput = string.Format(Layout.Format, dateTime, reportLevel, message) + Environment.NewLine;

            this.logFile.Write(ouput);
            this.AppendedMessages++;

            File.AppendAllText(Path, ouput);
        }

        public override string ToString()
        => $"{base.ToString()}, File size: {this.logFile.Size}";
    }
}
