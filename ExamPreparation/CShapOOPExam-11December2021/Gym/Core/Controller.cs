namespace Gym.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Gym.Core.Contracts;
    using Gym.Models.Athletes;
    using Gym.Models.Athletes.Contracts;
    using Gym.Models.Equipment;
    using Gym.Models.Equipment.Contracts;
    using Gym.Models.Gyms;
    using Gym.Models.Gyms.Contracts;
    using Gym.Repositories;
    using Gym.Repositories.Contracts;
    using Gym.Utilities.Messages;

    public class Controller : IController
    {
        private readonly IRepository<IEquipment> equipment;
        private readonly ICollection<IGym> gyms;

        public Controller()
        {
            equipment = new EquipmentRepository();
            gyms = new List<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gym = gyms.FirstOrDefault(x => x.Name == gymName);
            IAthlete athlete = null;
            if (athleteType == nameof(Boxer))
            {
                if (gym.GetType().Name == nameof(BoxingGym))
                {
                    athlete = new Boxer(athleteName, motivation, numberOfMedals);
                }
            }
            else if (athleteType == nameof(Weightlifter))
            {
                if (gym.GetType().Name == nameof(WeightliftingGym))
                {
                    athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            if (athlete == null)
            {
                return OutputMessages.InappropriateGym;
            }

            gym.AddAthlete(athlete);
            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;
            switch (equipmentType)
            {
                case "BoxingGloves": equipment = new BoxingGloves(); break;
                case "Kettlebell": equipment = new Kettlebell(); break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(equipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym;
            switch (gymType)
            {
                case "BoxingGym": gym = new BoxingGym(gymName); break;
                case "WeightliftingGym": gym = new WeightliftingGym(gymName); break;
                default: throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            double weight = gyms.FirstOrDefault(x => x.Name == gymName).EquipmentWeight;
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, $"{weight:f2}");
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var currentEquipment = equipment.FindByType(equipmentType);
            if (currentEquipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            gyms.FirstOrDefault(x => x.Name == gymName).AddEquipment(currentEquipment);
            equipment.Remove(currentEquipment);
            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            return string.Join(Environment.NewLine, gyms.Select(x => x.GymInfo()));
        }

        public string TrainAthletes(string gymName)
        {
            var gym = gyms.FirstOrDefault(x => x.Name == gymName);
            gym.Exercise();

            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count());
        }
    }
}
