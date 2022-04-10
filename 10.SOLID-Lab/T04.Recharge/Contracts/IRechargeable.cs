namespace T04.Recharge.Contracts
{
    public interface IRechargeable
    {
        int Capacity { get; }

        int CurrentPower { get; }

        void Recharge();
    }
}
