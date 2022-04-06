using T08.CollectionHierarchy.Contracts;

namespace T08.CollectionHierarchy.Models
{
    public class MyList : AddRemoveCollection, IMyList
    {
        public int Used => collection.Count;

        public override string Remove()
        {
            string item = collection[0];
            collection.RemoveAt(0);
            return item;
        }
    }
}
