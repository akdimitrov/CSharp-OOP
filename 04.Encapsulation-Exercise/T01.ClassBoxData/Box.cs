using System;
using System.Collections.Generic;
using System.Text;

namespace T01.ClassBoxData
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double lenght, double width, double height)
        {
            Lenght = lenght;
            Width = width;
            Height = height;
        }

        public double Lenght
        {
            get { return length; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Length cannot be zero or negative.");
                }
                else
                {
                    length = value;
                }
            }
        }

        public double Width
        {
            get { return width; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Width cannot be zero or negative.");
                }
                else
                {
                    width = value;
                }
            }
        }

        public double Height
        {
            get { return height; }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Height cannot be zero or negative.");
                }
                else
                {
                    height = value;
                }
            }
        }

        public double SurfaceArea()
        {
            return 2 * (length * width + width * height + length * height);
        }

        public double LateralSurfaceArea()
        {
            return 2 * (length * height + width * height);
        }

        public double Volume()
        {
            return length * width * height;
        }
    }
}
