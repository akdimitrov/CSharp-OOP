﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.GameObjects
{
    public class Snake
    {
        private const char snakeSymbol = '\u25CF';
        private readonly Queue<Point> snakeElements;
        private readonly Food[] food;
        private readonly Wall wall;
        private int foodIndex;
        private int nextLeftX;
        private int nextTopY;
        private int score;

        public Snake(Wall wall)
        {
            this.wall = wall;
            this.snakeElements = new Queue<Point>();
            this.food = new Food[3];
            this.GetFoods();
            this.CreateSnake();
        }

        public int Score => score;

        protected int RandomFoodNumber => new Random().Next(0, this.food.Length);

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();
            GetNextPoint(direction, currentSnakeHead);

            if (IsPointOfSnake())
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);

            if (IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(snakeSymbol);

            if (food[foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.Eat(direction, currentSnakeHead);
                this.score += food[foodIndex].FoodPoints;
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = food[foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void GetFoods()
        {
            this.food[0] = new FoodHash(this.wall);
            this.food[1] = new FoodDollar(this.wall);
            this.food[2] = new FoodAsterisk(this.wall);
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        private bool IsPointOfWall(Point snake)
        {
            return snake.TopY == 0 || snake.LeftX == 0 || snake.LeftX == this.wall.LeftX - 1 || snake.TopY == this.wall.TopY;
        }

        private bool IsPointOfSnake()
        {
            return this.snakeElements.Any(x => x.LeftX == nextLeftX && x.TopY == nextTopY);
        }
    }
}
