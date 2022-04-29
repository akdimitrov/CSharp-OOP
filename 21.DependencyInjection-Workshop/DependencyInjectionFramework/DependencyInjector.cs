using DependencyInjectionFramework.Injectors;
using DependencyInjectionFramework.Modules;

namespace DependencyInjectionFramework
{
    public class DependencyInjector
    {
        public static Injector CreateInjector(IModule module)
        {
            module.Configure();
            return new Injector(module);
        }
    }
}
