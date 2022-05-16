using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double armorThickness;
        private readonly double initialArmorThickness;

        protected Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            initialArmorThickness = armorThickness;
            Targets = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }

                name = value;
            }
        }

        public ICaptain Captain
        {
            get => captain;
            set
            {
                captain = value ?? throw new NullReferenceException(ExceptionMessages.InvalidCaptainToVessel);
            }
        }

        public double ArmorThickness
        {
            get => armorThickness;
            set
            {
                armorThickness = value;
                if (armorThickness < 0)
                {
                    armorThickness = 0;
                }
            }
        }

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets { get; }

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(ExceptionMessages.InvalidTarget);
            }

            target.ArmorThickness -= MainWeaponCaliber;
            Targets.Add(target.Name);
        }

        public void RepairVessel()
        {
            ArmorThickness = initialArmorThickness;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var targets = Targets.Any() ? string.Join(", ", Targets) : "None";
            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {Speed} knots");
            sb.AppendLine($" *Targets: {targets}");

            return sb.ToString().TrimEnd();
        }
    }
}
