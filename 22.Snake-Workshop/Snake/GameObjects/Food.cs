using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.GameObjects
{
    public abstract class Food : Point
    {
        private readonly Wall wall;
        private readonly Random random;
        private readonly char foodSymbol;


        protected Food(Wall wall, char foodSymbol, int foodPoints) : base(0, 0)
        {
            this.wall = wall;
            this.foodSymbol = foodSymbol;
            this.FoodPoints = foodPoints;
            this.random = new Random();
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snakeElements)
        {
            bool isPointOfSnake = false;
            do
            {
                this.LeftX = random.Next(1, wall.LeftX - 1);
                this.TopY = random.Next(1, wall.TopY - 1);
                isPointOfSnake = snakeElements.Any(x => x.LeftX == this.LeftX && x.TopY == this.TopY);
            } while (isPointOfSnake);

            Console.BackgroundColor = ConsoleColor.Green;
            this.Draw(foodSymbol);
            Console.BackgroundColor = ConsoleColor.White;
        }

        public bool IsFoodPoint(Point snake)
        {
            return snake.TopY == this.TopY && snake.LeftX == this.LeftX;
        }
    }
}
