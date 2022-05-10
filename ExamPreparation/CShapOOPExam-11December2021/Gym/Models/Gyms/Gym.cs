namespace Gym.Models.Gyms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Athletes.Contracts;
    using Equipment.Contracts;
    using Gyms.Contracts;
    using Utilities.Messages;

    public abstract class Gym : IGym
    {
        private string name;

        public Gym(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            Equipment = new List<IEquipment>();
            Athletes = new List<IAthlete>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGymName);
                }

                name = value;
            }
        }

        public int Capacity { get; }

        public double EquipmentWeight => Equipment.Sum(x => x.Weight);

        public ICollection<IEquipment> Equipment { get; }

        public ICollection<IAthlete> Athletes { get; }


        public void AddAthlete(IAthlete athlete)
        {
            if (Capacity <= Athletes.Count)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughSize);
            }

            Athletes.Add(athlete);
        }

        public bool RemoveAthlete(IAthlete athlete)
        {
            return Athletes.Remove(athlete);
        }

        public void AddEquipment(IEquipment equipment)
        {
            Equipment.Add(equipment);
        }

        public void Exercise()
        {
            foreach (var athlete in Athletes)
            {
                athlete.Exercise();
            }
        }

        public string GymInfo()
        {
            string athletes = !Athletes.Any() ? "No athletes" :
                string.Join(", ", Athletes.Select(x => x.FullName));
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} is a {GetType().Name}:");
            sb.AppendLine($"Athletes: {athletes}");
            sb.AppendLine($"Equipment total count: {Equipment.Count}");
            sb.AppendLine($"Equipment total weight: {EquipmentWeight:f2} grams");

            return sb.ToString().TrimEnd();
        }
    }
}
