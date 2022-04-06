using System.Collections.Generic;
using T08.CollectionHierarchy.Contracts;

namespace T08.CollectionHierarchy.Models
{
    public class AddCollection : IAddCollection
    {
        protected List<string> collection;

        public AddCollection()
        {
            collection = new List<string>();
        }

        public virtual int Add(string item)
        {
            collection.Add(item);
            return collection.Count - 1;
        }
    }
}
