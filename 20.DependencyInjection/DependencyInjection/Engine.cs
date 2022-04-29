using DependencyInjection.Contracts;

namespace DependencyInjection
{
    public class Engine
    {
        private readonly IReader reader;
        private readonly ILogger logger;

        public Engine(IReader reader, ILogger logger)
        {
            this.reader = reader;
            this.logger = logger;
        }


        public void Start()
        {
            while (true)
            {
                reader.Read();
                logger.Log("running");
            }
        }
    }
}
