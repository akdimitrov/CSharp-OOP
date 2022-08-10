namespace Logger.Appenders
{
    using System;
    using System.IO;
    using Layouts;
    using LogFiles;
    using ReportLevels;

    public class FileAppender : Appender
    {
        private readonly string filePath;
        private readonly ILogFile logFile;

        public FileAppender(ILayout layout, ILogFile logFile, string filePath = "../../../log.txt")
            : base(layout)
        {
            this.logFile = logFile;
            this.filePath = filePath;
        }

        public override void Append(DateTime dateTime, ReportLevel reportLevel, string message)
        {
            string ouput = string.Format(Layout.Format, dateTime, reportLevel, message) + Environment.NewLine;

            this.logFile.Write(ouput);
            this.AppendedMessages++;

            File.AppendAllText(filePath, ouput);
        }

        public override string ToString()
        => $"{base.ToString()}, File size: {this.logFile.Size}";
    }
}
