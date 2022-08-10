namespace Logger.Loggers
{
    using System.Collections.Generic;
    using Appenders;

    public interface ILogger
    {
        IReadOnlyCollection<IAppender> Appenders { get; }

        void Info(string message);

        void Warning(string message);

        void Error(string message);

        void Critical(string message);

        void Fatal(string message);
    }
}
