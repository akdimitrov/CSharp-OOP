using System;
using CustomDependencyInjection;
using DependencyInjection.Contracts;

namespace DependencyInjection
{
    public class DICustomConfigurator : ServiceModule
    {
        public override void Configure()
        {
            CreateMapping<IReader, ConsoleReader>();
            CreateMapping<Engine, Engine>();
            CreateMapping<ILogger, Logger>();
            CreateMapping<DateTime>(() =>
            {
                return DateTime.Now;
            });
        }
    }
}
