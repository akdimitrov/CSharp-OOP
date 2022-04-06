using System;
using System.Collections.Generic;
using System.Text;
using T08.CollectionHierarchy.Contracts;
using T08.CollectionHierarchy.Models;

namespace T08.CollectionHierarchy.Core
{
    public class Engine
    {
        private AddCollection addCollection;
        private AddRemoveCollection addRemoveCollection;
        private MyList myList;
        private StringBuilder result;
        private string[] items;
        private int removeCount;

        public Engine()
        {
            addCollection = new AddCollection();
            addRemoveCollection = new AddRemoveCollection();
            myList = new MyList();
            result = new StringBuilder();
        }

        public void Run()
        {
            items = Console.ReadLine().Split();
            removeCount = int.Parse(Console.ReadLine());

            AddItems(addCollection);
            AddItems(addRemoveCollection);
            AddItems(myList);
            RemoveItems(addRemoveCollection);
            RemoveItems(myList);

            Console.WriteLine(result.ToString().TrimEnd());
        }

        private void AddItems(IAddCollection collection)
        {
            foreach (var item in items)
            {
                result.Append($"{collection.Add(item)} ");
            }

            result.AppendLine();
        }

        private void RemoveItems(IAddRemoveCollection collection)
        {
            for (int i = 0; i < removeCount; i++)
            {
                result.Append($"{collection.Remove()} ");
            }

            result.AppendLine();
        }
    }
}
