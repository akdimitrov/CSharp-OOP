using System;
using Easter.Models.Dyes.Contracts;

namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        private int power;

        public Dye(int power)
        {
            Power = power;
        }

        public int Power
        {
            get => power;
            private set
            {
                power = Math.Max(value, 0);
            }
        }

        public bool IsFinished()
        {
            return Power == 0;
        }

        public void Use()
        {
            Power -= 10;
        }
    }
}
