using System;
using System.Linq;
using System.Text;
using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using Formula1.Utilities;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IPilot> pilotRepository;
        private readonly IRepository<IRace> raceRepository;
        private readonly IRepository<IFormulaOneCar> carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = pilotRepository.FindByName(pilotName);
            if (pilot == null || pilot.CanRace)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            var car = carRepository.FindByName(carModel);
            if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            carRepository.Remove(car);
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var race = raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var pilot = pilotRepository.FindByName(pilotFullName);
            if (pilot == null || !pilot.CanRace || race.Pilots.Any(x => x.FullName == pilotFullName))
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            IFormulaOneCar car;
            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else if (type == nameof(Williams))
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            carRepository.Add(car);
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            if (pilotRepository.FindByName(fullName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            pilotRepository.Add(new Pilot(fullName));
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.FindByName(raceName) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            raceRepository.Add(new Race(raceName, numberOfLaps));
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.FindByName(raceName);
            if (race == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            else if (race.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var top3 = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3).ToList();
            top3.First().WinRace();
            race.TookPlace = true;

            StringBuilder result = new StringBuilder();
            result.AppendLine(string.Format(OutputMessages.PilotFirstPlace, top3[0].FullName, raceName));
            result.AppendLine(string.Format(OutputMessages.PilotSecondPlace, top3[1].FullName, raceName));
            result.AppendLine(string.Format(OutputMessages.PilotThirdPlace, top3[2].FullName, raceName));

            return result.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder pilotReport = new StringBuilder();
            foreach (var pilot in pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                pilotReport.AppendLine(pilot.ToString());
            }

            return pilotReport.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder raceReport = new StringBuilder();
            foreach (var race in raceRepository.Models.Where(x => x.TookPlace))
            {
                raceReport.AppendLine(race.RaceInfo());
            }

            return raceReport.ToString().TrimEnd();
        }
    }
}
