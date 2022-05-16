namespace NavalVessels.Core
{
    using System;
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
                if (input[0] == "Quit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "HireCaptain")
                    {
                        string captainFullName = input[1];
                        string result = controller.HireCaptain(captainFullName);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "ProduceVessel")
                    {
                        string name = input[1];
                        string vesselType = input[2];
                        double mainWeaponCaliber = double.Parse(input[3]);
                        double speed = double.Parse(input[4]);

                        string result = controller.ProduceVessel(name, vesselType, mainWeaponCaliber, speed);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "AssignCaptain")
                    {
                        string selectedCaptainName = input[1];
                        string selectedVesselName = input[2];

                        string result = controller.AssignCaptain(selectedCaptainName, selectedVesselName);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "CaptainReport")
                    {
                        string selectedCaptainName = input[1];

                        string result = controller.CaptainReport(selectedCaptainName);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "VesselReport")
                    {
                        string selectedVesselName = input[1];

                        string result = controller.VesselReport(selectedVesselName);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "ToggleSpecialMode")
                    {
                        string vessel = input[1];

                        string result = controller.ToggleSpecialMode(vessel);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "AttackVessels")
                    {
                        string attackingVesselName = input[1];
                        string defendingVesselName = input[2];

                        string result = controller.AttackVessels(attackingVesselName, defendingVesselName);
                        writer.WriteLine(result);
                    }
                    else if (input[0] == "ServiceVessel")
                    {
                        string vessel = input[1];

                        string result = controller.ServiceVessel(vessel);
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
