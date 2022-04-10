using T04.Recharge.Contracts;

namespace T04.Recharge
{
    public abstract class Worker : IWorker
    {
        public Worker(string id)
        {
            Id = id;
        }

        public string Id { get; }

        public int WorkingHours { get; private set; }

        public virtual void Work(int hours)
        {
            WorkingHours += hours;
        }
    }
}