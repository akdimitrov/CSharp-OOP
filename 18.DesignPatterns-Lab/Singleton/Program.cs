using System;

namespace Singleton
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = SingletonDataContainer.Instance;
            var db2 = SingletonDataContainer.Instance;
            Console.WriteLine(db.GetHashCode() == db.GetHashCode());

            Console.Write("Enter capital name: ");
            string capital = Console.ReadLine();
            while (capital != "END")
            {
                try
                {
                    Console.WriteLine($"Population of {capital} -> {db.GetPopulation(capital)}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.Write("Enter capital name: ");
                capital = Console.ReadLine();
            }
        }
    }
}
