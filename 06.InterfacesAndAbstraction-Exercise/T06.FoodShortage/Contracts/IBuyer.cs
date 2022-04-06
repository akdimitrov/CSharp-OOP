using System;
using System.Collections.Generic;
using System.Text;

namespace T06.FoodShortage.Contracts
{
    public interface IBuyer
    {
        int Food { get; set; }

        void BuyFood();
    }
}
