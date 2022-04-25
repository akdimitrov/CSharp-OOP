using System;

namespace Facade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                  .WithType("BMW")
                  .WithColor("Black")
                  .WithNumberOfDoors(5)
                .Built
                  .InCity("Leipzig")
                  .AtAddress("The red house at the corner, behind the park")
                .Build();

            Console.WriteLine(car);
        }
    }
}
