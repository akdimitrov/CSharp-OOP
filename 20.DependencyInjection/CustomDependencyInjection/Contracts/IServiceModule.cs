using System;

namespace CustomDependencyInjection.Contracts
{
    public interface IServiceModule
    {
        void CreateMapping<TInterface, IImplementation>();

        void CreateMapping<TInterface>(Func<object> createFunction);

        Type GetMapping<TInterface>();

        Type GetMapping(Type interfaceType);

        object GetCustomMapping(Type type);

        object GetInstance<TInterface>();

        void Configure();
    }
}
