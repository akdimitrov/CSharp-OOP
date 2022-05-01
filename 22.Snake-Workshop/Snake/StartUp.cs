using SnakeGame.Core;
using SnakeGame.GameObjects;
using SnakeGame.Utilities;

namespace SnakeGame
{
    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Wall wall = new Wall(60, 20);
            Snake snake = new Snake(wall);

            Engine engine = new Engine(wall, snake);
            engine.Run();
        }
    }
}
