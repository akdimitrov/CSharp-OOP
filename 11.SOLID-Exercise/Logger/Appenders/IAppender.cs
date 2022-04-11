namespace Logger.Appenders
{
    using System;
    using Layouts;
    using ReportLevels;

    public interface IAppender
    {
        ILayout Layout { get; }

        int AppendedMessages { get; }

        ReportLevel ReportLevel { get; set; }

        void Append(DateTime dateTime, ReportLevel reportLevel, string message);
    }
}
