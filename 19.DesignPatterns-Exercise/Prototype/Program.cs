namespace Prototype
{
    public class Program
    {
        static void Main(string[] args)
        {
            SandwichMenu sandwichMenu = new SandwichMenu();

            // Initialize with default sandwiches
            sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato");
            sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly");
            sandwichMenu["Turkey"] = new Sandwich("Rye", "Turkey", "Swiss", "Lettuce, Onion, Tomato");

            // Deli manager adds custom sandwiches
            sandwichMenu["LoadedBLT"] = new Sandwich("Wheat", "Turkey, Bacon", "American", "Letuce, Tomato, Onion, Olives");
            sandwichMenu["ThreeMeatCombo"] = new Sandwich("Rye", "Turkey, Ham, Salami", "Provolone", "Letuce, Tomato, Onion");
            sandwichMenu["Vegetarian"] = new Sandwich("Wheat", "", "", "Letuce, Onion, Tomato, Olives, Spinach");

            // Now we can clone these sandwiches
            Sandwich sandwich1 = sandwichMenu["BLT"].DeepCopy();
            Sandwich sandwich2 = sandwichMenu["ThreeMeatCombo"].ShallowCopy();
            Sandwich sandwich3 = sandwichMenu["Vegetarian"].DeepCopy();
        }
    }
}
