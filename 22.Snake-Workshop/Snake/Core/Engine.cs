using System;
using System.Threading;
using SnakeGame.Enums;
using SnakeGame.GameObjects;

namespace SnakeGame.Core
{
    public class Engine
    {
        private readonly Wall wall;
        private readonly Snake snake;
        private readonly Point[] pointsOfDirection;
        private double sleepTime;
        private Direction direction;
        private readonly int leftX;
        private int topY;

        public Engine(Wall wall, Snake snake)
        {
            this.wall = wall;
            this.snake = snake;
            this.sleepTime = 100;
            this.pointsOfDirection = new Point[4];
            this.leftX = this.wall.LeftX + 1;
            this.topY = 3;
        }

        public void Run()
        {
            this.CreateDirections();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = snake.IsMoving(this.pointsOfDirection[(int)direction]);

                if (!isMoving)
                {
                    AskUserForRestart();
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)sleepTime);

                Console.SetCursorPosition(this.leftX, this.topY);
                Console.WriteLine($"Your score is: {snake.Score}");
            }
        }

        private void CreateDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            else if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
            }

            Console.CursorVisible = false;
        }

        private void AskUserForRestart()
        {
            Console.SetCursorPosition(this.leftX, ++this.topY);
            Console.Write("Would you like to continue? y/n ");
            string input = Console.ReadLine();

            if (input == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                StopGame();
            }
        }

        private void StopGame()
        {
            Console.SetCursorPosition(this.leftX, ++this.topY);
            Console.Write($"Game over!");
            Console.SetCursorPosition(this.leftX, this.wall.TopY);

            Environment.Exit(0);
        }
    }
}
