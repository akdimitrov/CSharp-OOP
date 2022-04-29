using System;

namespace DependencyInjectionFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Field)]
    public class Inject : Attribute
    {
    }
}
