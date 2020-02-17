using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrawShape
{
    public class cPolygon
    {
        private double cSize;
        public double r;
        public double scale;
        public void drawRegularPolygon(Panel pnlCanvas, PaintEventArgs e,
                          String diametr, String slides, Point o)
        {
            
            int s = int.Parse(slides);
            r = (int.Parse(diametr) / 2) * scale/100;

            Console.WriteLine("...........r " + r);
            Point circleCenter = o;
            circleCenter.x = (pnlCanvas.Width / 2);
            circleCenter.y = (pnlCanvas.Height / 2);

            // BASIS OF THE FIGURE  // POINT LIST // FIST SEGMENT
            cSegment firstSegment = new cSegment(circleCenter, r, s);
            firstSegment.GetPointList();
            cSize = firstSegment.c;

            int point1X = firstSegment.GetPointList().ElementAt(0).x;
            int point1Y = firstSegment.GetPointList().ElementAt(0).y;
            int point2X = firstSegment.GetPointList().ElementAt(1).x;
            int point2Y = firstSegment.GetPointList().ElementAt(1).y;

            Pen bluePen = new Pen(Color.Blue, 3);
            PointF point1 = new PointF(point1X, point1Y);
            PointF point2 = new PointF(point2X, point2Y);
            PointF[] firstSlide =
             {
                point1,
                point2,
             };
            e.Graphics.DrawPolygon(bluePen, firstSlide);
            
            int pointNextX;
            int pointNextY;
            double setAngle;
            PointF point = new PointF(0, 0);
            Pen blackPen = new Pen(Color.Black, 2);

            for (int i = 0; i <= s-2; i++)
            {
                setAngle = i+1;                                                             // SETTING ANGLE BY ADDING THIS ANGLE EVERY SIDE
                if (i == 0)                 // SECOND SEGMENT
                {
                    cSegment nextSegment = new cSegment(point2, s, cSize, setAngle);        //POINT BEFORE; SIDE; SIZE OF SIDE; TOTAL ANGLE
                    pointNextX = nextSegment.GetPointList().ElementAt(0).x;
                    pointNextY = nextSegment.GetPointList().ElementAt(0).y;
                    nextSegment.GetPointList();
                    nextSegment.ShowPointList();

                    point.X = pointNextX;
                    point.Y = pointNextY;
                    PointF[] nextSlide =
                    {
                        point2,
                        point,
                     };
                    e.Graphics.DrawPolygon(blackPen, nextSlide);
                }
                else if (i == s-2)          // THE LAST SEGMENT
                {
                    cSegment nextSegment = new cSegment(point, s, cSize, setAngle);
                    pointNextX = nextSegment.GetPointList().ElementAt(0).x;
                    pointNextY = nextSegment.GetPointList().ElementAt(0).y;
                    nextSegment.GetPointList();
                    nextSegment.ShowPointList();

                    PointF[] nextSlide =
{
                        point,
                        point1,
                     };
                    e.Graphics.DrawPolygon(blackPen, nextSlide);
                    point.X = pointNextX;
                    point.Y = pointNextY;
                }
                else                         // NEXT SEGMENT
                {
                    cSegment nextSegment = new cSegment(point, s, cSize, setAngle);
                    pointNextX = nextSegment.GetPointList().ElementAt(0).x;
                    pointNextY = nextSegment.GetPointList().ElementAt(0).y;
                    nextSegment.GetPointList();
                    nextSegment.ShowPointList();

                    PointF pointNext = new PointF(pointNextX, pointNextY);
                    PointF[] nextSlide =
                     {
                        point,
                        pointNext,
                     };
                    e.Graphics.DrawPolygon(blackPen, nextSlide);
                    point.X = pointNextX;
                    point.Y = pointNextY;
                }
            }
        }


        public void drawRectangle(Panel pnlCanvas, PaintEventArgs e,
                              String txtWidth, String txtHeight, String txtMarginV, String txtMarginH)
        {

            int width = int.Parse(txtWidth);
            int height = int.Parse(txtHeight);
            int marginV = int.Parse(txtMarginV);
            int marginH = int.Parse(txtMarginH);

            if (width == 0 || height == 0)  //Blokada Dzielenia przez 0
            {
            }
            else
            {
                // Rozszerzanie na boki
                if (width >= height && pnlCanvas.Width >= pnlCanvas.Height)
                {
                    int wid = pnlCanvas.Height - marginH;
                    height = (height * wid) / width;
                    scale = (100 * wid) / width;
                    Console.WriteLine("1...........r " + scale + " " + wid + " " + width);
                    width = wid;
                    
                    
                }
                else if (width >= height && pnlCanvas.Width < pnlCanvas.Height)
                {
                    int wid = pnlCanvas.Width - marginV;
                    height = (height * wid) / width;
                    scale = (100 * wid) / width;
                    width = wid;
                    
                    Console.WriteLine("2...........r " + scale);
                }
                // Rozszerzanie góra - dół
                else if (width < height && pnlCanvas.Width >= pnlCanvas.Height)
                {
                    int hei = pnlCanvas.Height - marginH;
                    width = (width * hei) / height;
                    scale = (100 * hei) / height;
                    height = hei;
                    
                    Console.WriteLine("3...........r " + scale);
                }
                else if (width < height && pnlCanvas.Width < pnlCanvas.Height)
                {
                    int hei = pnlCanvas.Width - marginV;
                    width = (width * hei) / height;
                    scale = (100 * hei) / height;
                    height = hei;
                    
                    Console.WriteLine("4...........r " + scale);
                }
            }
            Console.WriteLine("sfddsfsf..........r " + scale);

            int startPointX = ((pnlCanvas.Width - width) / 2);
            int startPointY = ((pnlCanvas.Height - height) / 2);
            Point startPoint = new Point(startPointX, startPointY);
            cSegment newFigure = new cSegment(startPoint, width, height);
            newFigure.GetPointList();
          
            int point1X = newFigure.GetPointList().ElementAt(0).x;
            int point1Y = newFigure.GetPointList().ElementAt(0).y;
            int point2X = newFigure.GetPointList().ElementAt(2).x;
            int point2Y = newFigure.GetPointList().ElementAt(2).y;

            Pen blackPen = new Pen(Color.Black, 2);
            PointF point1 = new PointF(point1X, point1Y);
            PointF point2 = new PointF(point2X, point1Y);
            PointF point3 = new PointF(point2X, point2Y);
            PointF point4 = new PointF(point1X, point2Y);

            PointF[] curvePoints =
             {
                point1,
                point2,
                point3,
                point4,
             };
            e.Graphics.DrawPolygon(blackPen, curvePoints);
        }


    }
}