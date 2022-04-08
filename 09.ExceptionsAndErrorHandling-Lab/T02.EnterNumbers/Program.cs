using System;
using System.Collections.Generic;

namespace T02.EnterNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            do
            {
                int start = numbers.Count == 0 ? 1 : numbers[^1];
                try
                {
                    numbers.Add(ReadNumber(start));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            } while (numbers.Count < 10);

            Console.WriteLine(string.Join(", ", numbers));
        }

        static int ReadNumber(int start, int end = 100)
        {
            bool success = int.TryParse(Console.ReadLine(), out int n);
            if (!success)
            {
                throw new ArgumentException("Invalid Number!");
            }
            if (n <= start || n >= end)
            {
                throw new ArgumentException($"Your number is not in range {start} - 100!");
            }

            return n;
        }
    }
}
