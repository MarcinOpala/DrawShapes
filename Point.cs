using System;

namespace DrawShape
{
    public class Point
    {
        public int x;
        public int y;



        public Point(int x1, int y1)
        {
            this.x = x1;
            this.y = y1;
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
