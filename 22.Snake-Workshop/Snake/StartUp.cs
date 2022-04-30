using Snake.Core;
using Snake.GameObjects;
using Snake.Utilities;

namespace Snake
{
    public class StartUp
    {
        public static void Main()
        {
            ConsoleWindow.CustomizeConsole();

            Wall wall = new Wall(60, 20);
            GameObjects.Snake snake = new GameObjects.Snake(wall);

            Engine engine = new Engine(wall, snake);
            engine.Run();
        }
    }
}
