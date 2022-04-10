namespace T04.Recharge.Contracts
{
    public interface IWorker
    {
        string Id { get; }

        int WorkingHours { get; }

        void Work(int hours);
    }
}
