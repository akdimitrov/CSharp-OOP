using System;
using DependencyInjection.Contracts;

namespace DependencyInjection
{
    public class SpecialLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine($"You are special! {log}");
        }
    }
}
