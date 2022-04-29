using System;
using DependencyInjection.Contracts;

namespace DependencyInjection
{
    public class Logger : ILogger
    {
        private readonly DateTime dateToday;

        public Logger(DateTime dateToday)
        {
            this.dateToday = dateToday;
        }

        public void Log(string log)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine($"{dateToday.ToString("dd/MM/yy hh:mm:ss")} {log}");
        }
    }
}
