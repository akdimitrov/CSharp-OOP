using System;

namespace T04.WildFarm.Exceptions
{
    public static class InvalidOperations
    {
        public static void ThrowInvalidFoodException(string animalType, string foodType)
        {
            throw new ArgumentException($"{animalType} does not eat {foodType}!");
        }
    }
}
