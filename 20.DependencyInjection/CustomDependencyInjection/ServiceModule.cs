using System;
using System.Collections.Generic;
using CustomDependencyInjection.Contracts;

namespace CustomDependencyInjection
{
    public abstract class ServiceModule : IServiceModule
    {
        private readonly Dictionary<Type, Type> mappings;
        private readonly Dictionary<Type, Func<object>> customMappings;
        private readonly Dictionary<Type, object> instances;

        public ServiceModule()
        {
            mappings = new Dictionary<Type, Type>();
            customMappings = new Dictionary<Type, Func<object>>();
            instances = new Dictionary<Type, object>();
            Configure();
        }

        public void CreateMapping<TInterface, TImplementation>()
        {
            mappings[typeof(TInterface)] = typeof(TImplementation);
        }

        public void CreateMapping<TInterface>(Func<object> createFunction)
        {
            customMappings[typeof(TInterface)] = createFunction;
        }

        public abstract void Configure();

        public object GetInstance<TInterface>()
        {
            if (instances.ContainsKey(typeof(TInterface)))
            {
                return instances[typeof(TInterface)];
            }

            return null;
        }

        public Type GetMapping<TInterface>()
        {
            return mappings[typeof(TInterface)];
        }

        public Type GetMapping(Type interfaceType)
        {
            if (!mappings.ContainsKey(interfaceType))
            {
                return null;
            }

            return mappings[interfaceType];
        }

        public object GetCustomMapping(Type type)
        {
            return customMappings[type]();
        }
    }
}
