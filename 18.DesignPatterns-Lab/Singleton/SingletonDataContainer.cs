using System;
using System.Collections.Generic;
using System.IO;

namespace Singleton
{
    public class SingletonDataContainer : ISingletonContainer
    {
        private static SingletonDataContainer instance;
        private readonly Dictionary<string, int> capitals;
        private static readonly object padlock = new object();

        private SingletonDataContainer()
        {
            capitals = new Dictionary<string, int>();
            Console.WriteLine("Initializing singleton object");
            var elements = File.ReadAllLines("../../../capitals.txt");
            for (int i = 0; i < elements.Length; i += 2)
            {
                capitals.Add(elements[i], int.Parse(elements[i + 1]));
            }
        }

        public static SingletonDataContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonDataContainer();
                        }
                    }
                }

                return instance;
            }
        }

        public int GetPopulation(string name)
        {
            if (!capitals.ContainsKey(name))
            {
                throw new ArgumentException($"There is no data in the database for {name}");
            }

            return capitals[name];
        }
    }
}
