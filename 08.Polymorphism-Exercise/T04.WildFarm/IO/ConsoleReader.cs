using System;
using T04.WildFarm.IO.Contracts;

namespace T04.WildFarm.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
