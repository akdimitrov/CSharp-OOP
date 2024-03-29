﻿namespace SnakeGame.GameObjects
{
    public class FoodDollar : Food
    {
        private const char foodSymbol = '$';
        private const int foodPoints = 2;

        public FoodDollar(Wall wall) : base(wall, foodSymbol, foodPoints)
        {
        }
    }
}
