namespace Gym.IO
{
    using System;
    using Gym.IO.Contracts;
    public class Reader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
