using T08.CollectionHierarchy.Contracts;

namespace T08.CollectionHierarchy.Models
{
    public class AddRemoveCollection : AddCollection, IAddRemoveCollection
    {
        public override int Add(string item)
        {
            collection.Insert(0, item);
            return 0;
        }

        public virtual string Remove()
        {
            string item = collection[collection.Count - 1];
            collection.RemoveAt(collection.Count - 1);
            return item;
        }
    }
}
