using System;
using System.Collections.Generic;
using System.Linq;
using T04.WildFarm.Exceptions;
using T04.WildFarm.Models.Contracts;

namespace T04.WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        public Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name { get; }

        public double Weight { get; protected set; }

        public int FoodEaten { get; protected set; }

        protected abstract IReadOnlyCollection<Type> PreferredFoods { get; }

        protected abstract double WeightMultiplier { get; }

        public void Eat(IFood food)
        {
            if (!PreferredFoods.Contains(food.GetType()))
            {
                throw new FoodNotPrefferedException(string.Format(ExceptionMessages.FoodNotPreffered, GetType().Name, food.GetType().Name));
            }

            Weight += food.Quantity * WeightMultiplier;
            FoodEaten += food.Quantity;
        }

        public abstract string ProduceSound();

        public override string ToString()
        {
            return $"{GetType().Name} [{Name}, ";
        }
    }
}
