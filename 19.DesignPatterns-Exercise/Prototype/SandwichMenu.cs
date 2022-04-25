using System.Collections.Generic;

namespace Prototype
{
    public class SandwichMenu
    {
        private readonly Dictionary<string, IPrototype<Sandwich>> sandwiches = new Dictionary<string, IPrototype<Sandwich>>();

        public IPrototype<Sandwich> this[string name]
        {
            get => sandwiches[name];
            set => sandwiches.Add(name, value);
        }
    }
}
