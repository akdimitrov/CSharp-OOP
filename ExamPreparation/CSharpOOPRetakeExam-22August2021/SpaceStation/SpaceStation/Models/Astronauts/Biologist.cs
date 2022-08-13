namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double InitialOxygen = 70;
        public Biologist(string name)
            : base(name, InitialOxygen)
        {
        }

        public override void Breath()
            => Oxygen = Oxygen - 5 > 0 ? Oxygen -= 5 : 0;
    }
}
