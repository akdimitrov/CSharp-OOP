namespace T03.Raiding.Contractcs
{
    public interface IHero
    {
        string Name { get; set; }

        int Power { get; set; }

        string CastAbility();
    }
}
