using System;
using T03.Telephony.Models;

namespace T03.Telephony.Core
{
    public class Engine
    {
        private string[] phoneNumbers;
        private string[] urls;
        private Smartphone smartphone;
        private StationaryPhone stationaryPhone;

        public Engine()
        {
            smartphone = new Smartphone();
            stationaryPhone = new StationaryPhone();
        }

        public void Run()
        {
            phoneNumbers = Console.ReadLine().Split();
            urls = Console.ReadLine().Split();

            CallNumbers();
            BrowseWebsites();
        }

        private void CallNumbers()
        {
            foreach (var number in phoneNumbers)
            {
                try
                {
                    if (number.Length == 10)
                    {
                        Console.WriteLine(smartphone.Call(number));
                    }
                    else
                    {
                        Console.WriteLine(stationaryPhone.Call(number));
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void BrowseWebsites()
        {
            foreach (var url in urls)
            {
                try
                {
                    Console.WriteLine(smartphone.Browse(url));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
