using System;
using System.Collections.Generic;
using System.Linq;

namespace T05.FootballTeamGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] cmdArgs = input.Split(';');
                string type = cmdArgs[0];
                string teamName = cmdArgs[1];
                Team team = teams.FirstOrDefault(x => x.Name == teamName);
                if (team == null && type != "Team")
                {
                    Console.WriteLine($"Team {teamName} does not exist.");
                    continue;
                }

                try
                {
                    if (type == "Team")
                    {
                        teams.Add(new Team(teamName));
                    }
                    else if (type == "Add")
                    {
                        string playerName = cmdArgs[2];
                        int endurance = int.Parse(cmdArgs[3]);
                        int sprint = int.Parse(cmdArgs[4]);
                        int dribble = int.Parse(cmdArgs[5]);
                        int passing = int.Parse(cmdArgs[6]);
                        int shooting = int.Parse(cmdArgs[7]);

                        team.AddPlayer(new Player(playerName, endurance, sprint, dribble, passing, shooting));
                    }
                    else if (type == "Remove")
                    {
                        string playerName = cmdArgs[2];
                        if (!team.RemovePlayer(playerName))
                        {
                            Console.WriteLine($"Player {playerName} is not in {teamName} team.");
                        }
                    }
                    else if (type == "Rating")
                    {
                        Console.WriteLine($"{team.Name} - {team.Rating}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
