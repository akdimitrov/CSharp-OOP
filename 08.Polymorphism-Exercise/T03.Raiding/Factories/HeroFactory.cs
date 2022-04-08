using T03.Raiding.Models.Heroes;

namespace T03.Raiding.Models.Factories
{
    public static class HeroFactory
    {
        public static BaseHero Create(string type, string name)
        {
            switch (type)
            {
                case "Druid": return new Druid(name);
                case "Paladin": return new Paladin(name);
                case "Rogue": return new Rogue(name);
                case "Warrior": return new Warrior(name);
                default: return null;
            }
        }
    }
}
