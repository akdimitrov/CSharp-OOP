using System;
using DependencyInjection.Contracts;

namespace DependencyInjection
{
    public class ConsoleReader : IReader
    {
        private readonly ILogger logger;

        public ConsoleReader(ILogger logger)
        {
            this.logger = logger;
        }

        public string Read()
        {
            logger.Log("Reading");
            return Console.ReadLine();
        }
    }
}
