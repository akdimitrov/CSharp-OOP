using System;

namespace T01.SquareRoot
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                uint n = uint.Parse(Console.ReadLine());
                Console.WriteLine(Math.Sqrt(n));
            }
            catch (OverflowException)
            {
                Console.WriteLine("Invalid number.");
            }
            finally
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
