using System;
using System.Collections.Generic;
using System.Linq;
using T07.MilitaryElite.Contracts;
using T07.MilitaryElite.Models;

namespace T07.MilitaryElite.Core
{
    public class Engine
    {
        private Dictionary<string, ISoldier> soldiers;

        public Engine()
        {
            soldiers = new Dictionary<string, ISoldier>();
        }

        public void Run()
        {
            string input = Console.ReadLine();
            while (input != "End")
            {
                string[] soldierInfo = input.Split();
                string type = soldierInfo[0];
                string id = soldierInfo[1];
                string firstName = soldierInfo[2];
                string lastName = soldierInfo[3];

                if (type == "Private")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    soldiers[id] = new Private(id, firstName, lastName, salary);
                }
                else if (type == "LieutenantGeneral")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    var soldier = new LieutenantGeneral(id, firstName, lastName, salary);
                    soldier.AddPrivates(soldiers, soldierInfo.Skip(5).ToArray());
                    soldiers[id] = soldier;
                }
                else if (type == "Engineer" && IsValidCorps(soldierInfo[5]))
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    string corps = soldierInfo[5];
                    var soldier = new Engineer(id, firstName, lastName, salary, corps);
                    soldier.AddRepairs(soldierInfo.Skip(6).ToArray());

                    soldiers[id] = soldier;
                }
                else if (type == "Commando" && IsValidCorps(soldierInfo[5]))
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    string corps = soldierInfo[5];
                    var soldier = new Commando(id, firstName, lastName, salary, corps);
                    soldier.AddMissions(soldierInfo.Skip(6).ToArray());

                    soldiers[id] = soldier;
                }
                else if (type == "Spy")
                {
                    int codeNumber = int.Parse(soldierInfo[4]);
                    var soldier = new Spy(id, firstName, lastName, codeNumber);
                    soldiers[id] = soldier;
                }

                input = Console.ReadLine();
            }

            foreach (var soldier in soldiers)
            {
                Console.WriteLine(soldier.Value.ToString());
            }
        }

        private bool IsValidCorps(string corps)
            => corps == "Airforces" || corps == "Marines";
    }
}
