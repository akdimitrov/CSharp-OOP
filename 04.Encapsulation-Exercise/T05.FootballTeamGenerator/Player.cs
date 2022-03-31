using System;
using System.Collections.Generic;
using System.Text;

namespace T05.FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Name = name;
            this.endurance = ValidateStat("Endurance", endurance);
            this.sprint = ValidateStat("Sprint", sprint);
            this.dribble = ValidateStat("Dribble", dribble);
            this.passing = ValidateStat("Passing", passing);
            this.shooting = ValidateStat("Shooting", shooting);
        }

        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("A name should not be empty.");
                }

                name = value;
            }
        }

        public double Stats => (endurance + sprint + dribble + passing + shooting) / 5d;

        private int ValidateStat(string name, int value)
        {
            if (value < 0 || value > 100)
            {
                throw new Exception($"{name} should be between 0 and 100.");
            }

            return value;
        }
    }
}
