using System;

namespace Prototype
{
    public class Sandwich : IPrototype<Sandwich>
    {
        private string bread;
        private string meat;
        private string cheese;
        private string veggies;

        public Sandwich(string bread, string meat, string cheese, string veggies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.veggies = veggies;

        }

        public Sandwich ShallowCopy()
        {
            Console.WriteLine($"Clonning sandwich ingredients: {this.GetIngredientList()}");
            return MemberwiseClone() as Sandwich;
        }

        public Sandwich DeepCopy()
        {
            Console.WriteLine($"Clonning sandwich ingredients: {this.GetIngredientList()}");
            Sandwich copy = this.MemberwiseClone() as Sandwich;
            copy.bread = new string(bread);
            copy.meat = new string(meat);
            copy.cheese = new string(cheese);
            copy.veggies = new string(veggies);

            return copy;
        }

        private string GetIngredientList()
        => $"{this.bread}, {this.meat}, {this.cheese}, {this.veggies}";
    }
}
