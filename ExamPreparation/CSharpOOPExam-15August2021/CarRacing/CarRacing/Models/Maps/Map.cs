using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }

            if (!racerOne.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }

            if (!racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            racerOne.Race();
            racerTwo.Race();

            var racerOneChanseOfWinning =
                racerOne.Car.HorsePower *
                racerOne.DrivingExperience *
                (racerOne.RacingBehavior == "strict" ? 1.2
                : racerOne.RacingBehavior == "aggressive" ? 1.1
                : 1);

            var racerTwoChanseOfWinning =
                racerTwo.Car.HorsePower *
                racerTwo.DrivingExperience *
                (racerTwo.RacingBehavior == "strict" ? 1.2
                : racerTwo.RacingBehavior == "aggressive" ? 1.1
                : 1);

            var winnerUsername = racerOneChanseOfWinning > racerTwoChanseOfWinning ? racerOne.Username : racerTwo.Username;

            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winnerUsername);
        }
    }
}
