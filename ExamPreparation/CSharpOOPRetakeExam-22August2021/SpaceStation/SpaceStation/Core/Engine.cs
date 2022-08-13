namespace SpaceStation.Core
{
    using System;
    using System.Linq;

    using Contracts;
    using IO;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IReader reader;
        private readonly IController controller;

        public Engine()
        {
            writer = new Writer();
            reader = new Reader();
            controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();
                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "AddAstronaut")
                    {
                        string astronautType = input[1];
                        string astronautName = input[2];

                        string result = controller.AddAstronaut(astronautType, astronautName);

                        writer.WriteLine(result);
                    }
                    else if (input[0] == "AddPlanet")
                    {
                        string planetName = input[1];
                        string[] items = input
                            .Skip(2)
                            .ToArray();

                        string result = controller.AddPlanet(planetName, items);

                        writer.WriteLine(result);
                    }
                    else if (input[0] == "RetireAstronaut")
                    {
                        string astronautName = input[1];

                        string result = controller.RetireAstronaut(astronautName);

                        writer.WriteLine(result);
                    }
                    else if (input[0] == "ExplorePlanet")
                    {
                        string planetName = input[1];

                        string result = controller.ExplorePlanet(planetName);

                        writer.WriteLine(result);
                    }
                    else if (input[0] == "Report")
                    {
                        string result = controller.Report();

                        writer.WriteLine(result);
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
