using CustomDependencyInjection;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            Injector.Instance.ServiceModule = new DICustomConfigurator();
            Engine engine = Injector.Instance.Create<Engine>();
            engine.Start();
        }
    }
}
