namespace T04.Recharge
{
    using T04.Recharge.Contracts;

    public class Employee : Worker, ISleeper
    {
        public Employee(string id) : base(id) { }

        public void Sleep()
        {
            // sleep...
        }
    }
}
