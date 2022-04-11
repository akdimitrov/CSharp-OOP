namespace Logger.Appenders
{
    using System;
    using Layouts;
    using ReportLevels;

    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout) : base(layout) { }

        public override void Append(DateTime dateTime, ReportLevel reportLevel, string message)
        {
            string ouput = string.Format(Layout.Format, dateTime, reportLevel, message);

            Console.WriteLine(ouput);
            this.AppendedMessages++;
        }
    }
}
