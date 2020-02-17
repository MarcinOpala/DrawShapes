using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape
{
    class cSegment
    {
        public double size;
        public double radius;
        public int side;
        public double c;

        public Point circleCenter = new Point(0, 0);
        public Point before = new Point(0, 0);
        public Point next = new Point(0, 0);

        public List<Point> pointsList = new List<Point>();
        public cSegment() {}


                  // >>>> FIRST SEGMENT OF REGULAR POLYGON <<<<
        public cSegment(Point o, double r, int s)           
        {
            this.radius = r;
            this.side = s;
            this.circleCenter = o;
                                                            //MATHEMATICAL FORMULA:
            double b = (360 / s)/2;                         //ANGLE
            double a = (b * (Math.PI)) / 180;               //ANGLE -RADIAN
            double cosA_divBy2 = Math.Cos(a);               
            double sinA_divBy2 = Math.Sin(a);
            double h = r - (r*(1 - cosA_divBy2));           //DISTANCE - CIRCLE: FROM (o.x, o.y) TO (o.x, first.y)
            c = 2 * r * sinA_divBy2;

            Point first = new Point(0, 0);
            first.x = o.x - (int)(c/2) ;
            first.y = o.y + (int)h  ;

            Point second = new Point(0, 0);
            second.x = o.x + (int)(c / 2);
            second.y = first.y;
            
            pointsList.Insert(0, first);
            pointsList.Insert(1, second);
        }


                  // >>>> POINT TO NEXT -> LAST SEGMENT OF REGULAR POLYGON <<<<
        public cSegment(PointF before, int s, double c, double setAngle)      
        {                                                                     //  - find point before, [+/- cosinus/sinus (full alfa)] * c
            this.size = c;                                                    //SIZE OF ONE SIDE
            this.side = s;                                                    //NUMBER OF ALL SIDE
            double b = (360 / s) * setAngle;                                  //TOTAL ANGLE
            double a = (b * (Math.PI)) / 180;
            double cosA = Math.Cos(a);
            double sinA = Math.Sin(a);

            Point next = new Point(0, 0);
            next.x = (int)before.X + (int)(c*cosA);
            next.y = (int)before.Y - (int)(c*sinA);
            pointsList.Insert(0, next);
        }


                 // >>>> POINT TO SIMPLE LINE <<<<
        public Point first = new Point(0, 0);
        public Point second = new Point(0, 0);
        public cSegment(Point p1, Point p2)
        {
            this.first = p1;
            this.second = p2;
        }


                 // >>>> POINT TO RECTANGLE <<<<
        public int sizeWidth;
        public int sizeHeight;
        public cSegment(Point point, int width, int height)
        {
            this.sizeWidth = width;
            this.sizeHeight = height;

            // ADD POINTS TO LIST + DISPLAY
            Point firstPoint = new Point(point.x, point.y + height);
            Point secondPoint = new Point(point.x, point.y);
            Point thirdPoint = new Point(point.x + width, point.y);
            Point fourthPoint = new Point(point.x + width, point.y + height);
            pointsList.Insert(0, firstPoint);
            pointsList.Insert(1, secondPoint);
            pointsList.Insert(2, thirdPoint);
            pointsList.Insert(3, fourthPoint);
        //  ShowPointList();
/*
            // ADD LINES TO LIST + DISPLAY
            Line newLine1 = new Line(firstPoint, secondPoint);
            Line newLine2 = new Line(secondPoint, thirdPoint);
            Line newLine3 = new Line(thirdPoint, fourthPoint);
            Line newLine4 = new Line(fourthPoint, firstPoint);
            linesList.Insert(0, newLine1);
            linesList.Insert(1, newLine2);
            linesList.Insert(2, newLine3);
            linesList.Insert(3, newLine4);
*/
        }




        public String ToString()
        {
            return null; //  "x1: " + first.x + " y1: " + first.y + "\nx2: " + second.x + " y2: " + second.y;
        }

        public List<Point> GetPointList()
        {

            return pointsList;
        }

/*        public List<Line> GetLineList()
        {

            return linesList;
        }*/

        public void ShowPointList()
        {
            foreach (var i in pointsList)
            {
                Console.WriteLine("PUNKT: " + (pointsList.IndexOf(i) + 1) + "\n( " + i.x + " ; " + i.y + " )");
                Console.WriteLine();
            }
        }

/*        public void ShowLinesList()
        {
            foreach (var i in linesList)
            {
                Console.WriteLine("Linia: " + (linesList.IndexOf(i) + 1) +
                                 "\nP1: ( " + i.first.x + " ; " + i.first.y + " )" +
                                 "\nP2: ( " + i.second.x + " ; " + i.second.y + " )");
                Console.WriteLine();
            }
        }*/
    }
}
