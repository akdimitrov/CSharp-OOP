using System;
using System.Collections.Generic;
using System.Text;

namespace T04.PizzaCalories
{
    public class Dough
    {
        private const double CaloriesPerGram = 2;

        private readonly Dictionary<string, double> flourTypeModifiers = new Dictionary<string, double>()
        {
            ["white"] = 1.5,
            ["wholegrain"] = 1.0
        };

        private readonly Dictionary<string, double> bakingTechniqueModifiers = new Dictionary<string, double>()
        {
            ["crispy"] = 0.9,
            ["chewy"] = 1.1,
            ["homemade"] = 1.0
        };

        private string flourType;
        private string bakingTechnique;
        private int weight;

        public Dough(string flourType, string bakingTechnique, int weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public double Calories => CalculateCalories();

        public string FlourType
        {
            get => flourType;

            private set
            {
                if (!flourTypeModifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get => bakingTechnique;

            private set
            {
                if (!bakingTechniqueModifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                bakingTechnique = value;
            }
        }

        public int Weight
        {
            get => weight;

            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }

                weight = value;
            }
        }

        private double CalculateCalories()
        {
            return CaloriesPerGram
                * Weight
                * flourTypeModifiers[FlourType.ToLower()]
                * bakingTechniqueModifiers[BakingTechnique.ToLower()];
        }
    }
}
