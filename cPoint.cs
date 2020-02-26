using System;
using System.Drawing;

namespace DrawShape
{
    public class cPoint
    {
        public float x;
        public float y;



        public cPoint(PointF point)
        {
            this.x = point.X;
            this.y = point.Y;
        }

        public void PrintPoint (int x1, int y1)
        {
            this.x = x1;
            this.y = y1;

            Console.WriteLine("X: " + x);
            Console.WriteLine("Y: " + y);

        }

        public String ToString()
        {
            return "X: " + x + " X: " + y;
        }
    }




}
