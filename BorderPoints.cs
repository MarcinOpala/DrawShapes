using System;

namespace DrawShape
{
    public class BorderPoints {


        private static int x;
        private static int y;

        Point p1 = new Point(x, y);
        Point p2 = new Point(0,1);
        Point p3 = new Point(1,1);
        Point p4 = new Point(1,0);

        public BorderPoints()
        {

        }

        public void PrintBorder(Point punkt1, Point punkt2, Point punkt3, Point punkt4)
        {
            

            Console.WriteLine("P1 X: " + punkt1.x);
            Console.WriteLine("   Y: " + punkt1.y);

            Console.WriteLine("P2 X: " + punkt2.x);
            Console.WriteLine("   Y: " + punkt2.y);

            Console.WriteLine("P3 X: " + punkt3.x);
            Console.WriteLine("   Y: " + punkt3.y);

            Console.WriteLine("P4 X: " + punkt4.x);
            Console.WriteLine("   Y: " + punkt4.y);
                       
        }



    }
}
