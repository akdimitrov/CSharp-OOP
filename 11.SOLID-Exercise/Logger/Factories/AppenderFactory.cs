namespace Logger.Factories
{
    using System;
    using Appenders;
    using Layouts;
    using LogFiles;
    using ReportLevels;

    public static class AppenderFactory
    {
        public static IAppender CreateAppender(ILayout layout, string type, ReportLevel reportLevel)
        {
            IAppender appender = type switch
            {
                "FileAppender" => new FileAppender(layout, new LogFile()),
                "ConsoleAppender" => new ConsoleAppender(layout),
                _ => throw new InvalidOperationException("Missing type")
            };

            appender.ReportLevel = reportLevel;
            return appender;
        }
    }
}
