using System;

namespace DrawShape
{
    public class Line
    {

        public string typeOfLine;
        public string colourOfLine;

        public Point first = new Point(0, 0);
        public Point second = new Point(0, 0);

        public Line(Point p1, Point p2)
        {
            this.first = p1;
            this.second = p2;
        }
        
        public String ToString()
        {
            return "x1: " + first.x + " y1: " + first.y + "\nx2: " + second.x + " y2: " + second.y;
        }
    }
}


