using PlanetWars.IO.Contracts;
using System;
using System.IO;

namespace PlanetWars.IO
{
    public class Writer : IWriter
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
