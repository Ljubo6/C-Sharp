using System;
using System.Collections.Generic;
using System.Text;

namespace _09.RectangleIntersection
{
    public class Rectangle
    {
        private string id;
        private double width;
        private double height;
        private double x;
        private double y;

        public Rectangle(string id,double width,double hight,double x,double y)
        {
            this.Id = id;
            this.Width = width;
            this.Height = hight;
            this.X = x;
            this.Y = y;
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        internal bool Intersect(Rectangle secondRectangle)
        {
            if (this.X + this.Width < secondRectangle.X 
                || secondRectangle.X + secondRectangle.Width < this.X
                || this.Y + this.Height < secondRectangle.Y
                || secondRectangle.Y + secondRectangle.Height < this.Y)
            {
                return false;
            }
            return true;
        }
    }
}
