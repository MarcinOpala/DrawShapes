using System;
using System.Collections.Generic;

namespace DrawShape
{
    public class Rectangle              //CREATE POINTS, LINES
    {
        private static int x;
        private static int y;
        public int sizeOfEdge;
        public int sizeWidth;
        public int sizeHeight;
        
        public List<Point> pointsList = new List<Point>();
        public List<Line> linesList = new List<Line>();
        Point startPoint = new Point(x, y);
        
        public Rectangle()
        {
        }


            public Rectangle(Point point, int width, int height)
        {
            this.startPoint = point;
            this.sizeWidth = width;
            this.sizeHeight = height;


            
            Point firstPoint = new Point(point.x, point.y + height);
            Point secondPoint = new Point(point.x, point.y);
            Point thirdPoint = new Point(point.x + width, point.y);
            Point fourthPoint = new Point(point.x + width, point.y + height);


            //////////////// TWORZĘ PIERWSZĄ LINIĘ
            Line newLine1 = new Line(firstPoint, secondPoint);
                newLine1.typeOfLine = "Linia Prosta";
                newLine1.colourOfLine = "Niebieski";
              //  newLine1.PrintLine();

            //////////////// TWORZĘ DRUGĄ LINIĘ
                Line newLine2 = new Line(secondPoint, thirdPoint);
                newLine2.typeOfLine = "Linia Prosta";
                newLine2.colourOfLine = "Niebieski";
               // newLine2.PrintLine();

            //////////////// TWORZĘ TRZECIĄ LINIĘ
            Line newLine3 = new Line(thirdPoint, fourthPoint);
                newLine3.typeOfLine = "Linia Prosta";
                newLine3.colourOfLine = "Niebieski";
             //   newLine3.PrintLine();

            //////////////// TWORZĘ CZWARTĄ LINIĘ
            Line newLine4 = new Line(fourthPoint, firstPoint);
                newLine4.typeOfLine = "Linia Prosta";
                newLine4.colourOfLine = "Niebieski";
             //   newLine4.PrintLine();



            // ADD POINTS TO LIST + DISPLAY
            pointsList.Insert(0, firstPoint);
            pointsList.Insert(1, secondPoint);
            pointsList.Insert(2, thirdPoint);
            pointsList.Insert(3, fourthPoint);
       


            // ADD LINES TO LIST + DISPLAY
            linesList.Insert(0, newLine1);
            linesList.Insert(1, newLine2);
            linesList.Insert(2, newLine3);
            linesList.Insert(3, newLine4);

        }



        public List<Point> GetPointList()
        {

            return pointsList;
        }

        public List<Line> GetLineList()
        {

            return linesList;
        }

        public void ShowPointList()
        {
            foreach (var i in pointsList)
            {
                Console.WriteLine("PUNKT: " + (pointsList.IndexOf(i) + 1) + "\n( " + i.x + " ; " + i.y + " )");
                Console.WriteLine();
            }
        }

        public void ShowLinesList()
        {
            foreach (var i in linesList)
            {
                Console.WriteLine("Linia: " + (linesList.IndexOf(i) + 1) +
                                 "\nP1: ( " + i.first.x + " ; " + i.first.y + " )" +
                                 "\nP2: ( " + i.second.x + " ; " + i.second.y + " )");
                Console.WriteLine();
            }
        }

    }

}          
