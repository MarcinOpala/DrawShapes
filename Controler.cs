using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DrawShape
{
    public class Controler                  //CREATE startPoint, MAKE scale, DRAW
    {
        public void drawShape(object sender, Panel pnlCanvas, PaintEventArgs e,
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
                    width = wid;
                }
                else if (width >= height && pnlCanvas.Width < pnlCanvas.Height)
                {
                    int wid = pnlCanvas.Width - marginV;
                    height = (height * wid) / width;
                    width = wid;
                }
                // Rozszerzanie góra - dół
                else if (width < height && pnlCanvas.Width >= pnlCanvas.Height)
                {
                    int hei = pnlCanvas.Height - marginH;
                    width = (width * hei) / height;
                    height = hei;
                }
                else if (width < height && pnlCanvas.Width < pnlCanvas.Height)
                {
                    int hei = pnlCanvas.Width - marginV;
                    width = (width * hei) / height;
                    height = hei;
                }
            }

         
            int startPointX = ((pnlCanvas.Width - width) / 2) ;
            int startPointY = ((pnlCanvas.Height - height) / 2) ;
            Point startPoint = new Point(startPointX, startPointY);
            Rectangle newFigure = new Rectangle(startPoint, width, height);

            newFigure.GetPointList();
            newFigure.ShowPointList();

            Console.WriteLine( newFigure.GetPointList().ElementAt(0).ToString());
          

            // Lista punktów
            int point1X = newFigure.GetPointList().ElementAt(0).x;
            int point1Y = newFigure.GetPointList().ElementAt(0).y;
            int point2X = newFigure.GetPointList().ElementAt(1).x;
            int point2Y = newFigure.GetPointList().ElementAt(1).y;
            int point3X = newFigure.GetPointList().ElementAt(2).x;
            int point3Y = newFigure.GetPointList().ElementAt(2).y;
            int point4X = newFigure.GetPointList().ElementAt(3).x;
            int point4Y = newFigure.GetPointList().ElementAt(3).y;

            Pen blackPen = new Pen(Color.Black, 2);
            PointF point1 = new PointF(point1X, point1Y);
            PointF point2 = new PointF(point2X, point2Y);
            PointF point3 = new PointF(point3X, point3Y);
            PointF point4 = new PointF(point4X, point4Y);

            PointF[] curvePoints =
                     {
                point1,
                point2,
                point3,
                point4,

             };

            // Draw polygon curve to screen.
            e.Graphics.DrawPolygon(blackPen, curvePoints);


        }
    }
}