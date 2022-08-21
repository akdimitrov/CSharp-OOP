﻿using System;
using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string StreetRacerRacingBehavior = "aggressive";
        private const int StreetRacerDrivingExperience = 10;

        public StreetRacer(string username, ICar car) : base(username, StreetRacerRacingBehavior, StreetRacerDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 5;
        }
    }
}
