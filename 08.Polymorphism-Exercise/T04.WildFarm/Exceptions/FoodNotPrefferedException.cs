using System;

namespace T04.WildFarm.Exceptions
{
    public class FoodNotPrefferedException : Exception
    {
        public FoodNotPrefferedException()
        {
        }

        public FoodNotPrefferedException(string message)
            : base(message)
        {
        }
    }
}
