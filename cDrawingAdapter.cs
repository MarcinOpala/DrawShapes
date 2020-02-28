using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public class cDrawingAdapter {

    private static float mCenterPointOfSegmentX;                    //X środka segmentu
    private static float mCenterPointOfSegmentY;                    //Y środka segmentu
    private static int mCheckCirclePoint;                           //nr ćwiartki pCenterPointOfSegment liczona według schematu: 3|4
                                                                    //                                                           2|1 
    private static float mControlPoint_1X;                          //X punktu kotrolnego 1 do rysowania łuku 1:4 odcinka segmentu
    private static float mControlPoint_1Y;                          //Y punktu kotrolnego 1 do rysowania łuku
    private static float mControlPoint_2X;                          //X punktu kotrolnego 2 do rysowania łuku 
    private static float mControlPoint_2Y;                          //Y punktu kotrolnego 2 do rysowania łuku
    private static cPoint mHelperPoint = new cPoint(mPoint);
    private static int mNumberOfSides;                              //liczba boków figury
    private static PointF mPoint = new PointF();
    private static PointF[] mSegmentPoints = new PointF[2];         //inicjacja pomocniczej tablicy - dwa punkty każdego segmentu
    private static int mIsSelected;                                 //nr wybranego boku do przekształcenia w łuk
    private static double mVectorNormalX;                           //X wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych
    private static double mVectorNormalY;                           //Y wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych

    internal static int IsSelected { get { return mIsSelected; } set { mIsSelected = value; } }
    internal static PointF[] SegmentPoints { get { return mSegmentPoints; } set { mSegmentPoints = value; } }
    internal static cPoint HelperPoint { get { return mHelperPoint; } set { mHelperPoint = value; } }

    internal static void DrawWithOutSelectedSide(List<cSegment> xSegmentsList, PointF[] xSegmentPoints, int xSelected, PaintEventArgs e)
    {
      //funkcja rysująca figurę bez zaznaczonego boku 
      //xSegmentsList - lista wszystkich segmentów wybranej figury
      //xSegmentPoints - tablica 2 punktów, tworzących bok
      //xSelected - wybranie boku do "złukowania"

      Pen pBluePen = new Pen(Color.Blue, 3);
      mNumberOfSides = xSegmentsList.Count;

      //rysuję figurę do pIsSelected
      for (int i = 0; i <= xSelected - 2; i++)
      {
        if (i == (mNumberOfSides - 1))
        {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].PointOfSegment.X,
                                         xSegmentsList[i].PointOfSegment.Y);
          xSegmentPoints[1] = new PointF(xSegmentsList[0].PointOfSegment.X,
                                         xSegmentsList[0].PointOfSegment.Y);
        }
        else
        {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].PointOfSegment.X,
                                         xSegmentsList[i].PointOfSegment.Y);
          xSegmentPoints[1] = new PointF(xSegmentsList[i + 1].PointOfSegment.X,
                                         xSegmentsList[i + 1].PointOfSegment.Y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      //rysuję figurę od pIsSelected
      for (int i = xSelected; i <= mNumberOfSides - 1; i++)
      {
        if (i == (mNumberOfSides - 1))
        {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].PointOfSegment.X,
                                         xSegmentsList[i].PointOfSegment.Y);
          xSegmentPoints[1] = new PointF(xSegmentsList[0].PointOfSegment.X,
                                         xSegmentsList[0].PointOfSegment.Y);
        }
        else
        {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].PointOfSegment.X,
                                         xSegmentsList[i].PointOfSegment.Y);
          xSegmentPoints[1] = new PointF(xSegmentsList[i + 1].PointOfSegment.X,
                                         xSegmentsList[i + 1].PointOfSegment.Y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      // utworzenie dwóch dodatkowych punktów potrzebnych do narysowania łuku
      xSegmentPoints[0] = new PointF(xSegmentsList[xSelected - 1].PointOfSegment.X,
                                     xSegmentsList[xSelected - 1].PointOfSegment.Y);
      if (xSelected == mNumberOfSides) xSelected = 0;
      xSegmentPoints[1] = new PointF(xSegmentsList[xSelected].PointOfSegment.X,
                                     xSegmentsList[xSelected].PointOfSegment.Y);
    }

    internal static void DrawBezierCurve(PointF[] xSegmentPoints, PaintEventArgs e)
    {
      //funkcja rysująca łuk po wybraniu boku
      //xSegmentPoints - lista 2 punktów do narysowania boku

      // f(x)=0
      if ((xSegmentPoints[0].X - xSegmentPoints[1].X) == 0)
      {
        mVectorNormalX = Math.Abs((xSegmentPoints[1].Y - xSegmentPoints[0].Y) / 4);
        mVectorNormalY = 0;
      }
      // f(y)=0
      else if ((xSegmentPoints[0].Y - xSegmentPoints[1].Y) == 0)
      {
        mVectorNormalX = 0;
        mVectorNormalY = Math.Abs((xSegmentPoints[1].X - xSegmentPoints[0].X) / 4);
      }
      // f(y)=ax+b
      else
      {
        int pIncreaseVector = 50;
        mVectorNormalX = Math.Abs((xSegmentPoints[0].Y - xSegmentPoints[1].Y) /
                                  (xSegmentPoints[0].X - xSegmentPoints[1].X)) * pIncreaseVector;
        mVectorNormalY = pIncreaseVector;
      }
      mCenterPointOfSegmentX = (xSegmentPoints[0].X + xSegmentPoints[1].X) / 2;
      mCenterPointOfSegmentY = (xSegmentPoints[0].Y + xSegmentPoints[1].Y) / 2;
      Console.WriteLine(" S: (" + mCenterPointOfSegmentX + " ; " + mCenterPointOfSegmentY + ") ");
      SetCheckCirclePoint(mHelperPoint, (int)mCenterPointOfSegmentX, (int)mCenterPointOfSegmentY);
      
      //przesunięcie środka segmentu o wektor 
      //w zależności od ćwiartki w której się znajduję środek koła,
      //na którym opisana jest figura
      if (mCheckCirclePoint == 1)
      {
        mCenterPointOfSegmentX = mCenterPointOfSegmentX + (float)mVectorNormalX;
        mCenterPointOfSegmentY = mCenterPointOfSegmentY + (float)mVectorNormalY;
      }
      else if (mCheckCirclePoint == 2)
      {
        mCenterPointOfSegmentX = mCenterPointOfSegmentX - (float)mVectorNormalX;
        mCenterPointOfSegmentY = mCenterPointOfSegmentY + (float)mVectorNormalY;
      }
      else if (mCheckCirclePoint == 3)
      {
        mCenterPointOfSegmentX = mCenterPointOfSegmentX - (float)mVectorNormalX;
        mCenterPointOfSegmentY = mCenterPointOfSegmentY - (float)mVectorNormalY;
      }
      else if (mCheckCirclePoint == 4)
      {
        mCenterPointOfSegmentX = mCenterPointOfSegmentX + (float)mVectorNormalX;
        mCenterPointOfSegmentY = mCenterPointOfSegmentY - (float)mVectorNormalY;
      }
      else
      {
        mCenterPointOfSegmentX = mCenterPointOfSegmentX + (float)mVectorNormalX;
        mCenterPointOfSegmentY = mCenterPointOfSegmentY - (float)mVectorNormalY;
      }

      mControlPoint_1X = (xSegmentPoints[0].X + mCenterPointOfSegmentX) / 2;
      mControlPoint_1Y = (xSegmentPoints[0].Y + mCenterPointOfSegmentY) / 2;
      mControlPoint_2X = (mCenterPointOfSegmentX + xSegmentPoints[1].X) / 2;
      mControlPoint_2Y = (mCenterPointOfSegmentY + xSegmentPoints[1].Y) / 2;

      PointF start = new PointF(xSegmentPoints[0].X, xSegmentPoints[0].Y);
      PointF pControlPoint1 = new PointF(mControlPoint_1X, mControlPoint_1Y);
      PointF pControlPoint2 = new PointF(mControlPoint_2X, mControlPoint_2Y);
      PointF end = new PointF(xSegmentPoints[1].X, xSegmentPoints[1].Y);
      Pen pBluePen = new Pen(Color.Blue, 3);
      e.Graphics.DrawBezier(pBluePen, start, pControlPoint1, pControlPoint2, end);

      Console.WriteLine(" S: (" + mCenterPointOfSegmentX + " ; " + mCenterPointOfSegmentY + ") \n" +
                       " S1: (" + mControlPoint_1X + " ; " + mControlPoint_1Y + ") \n" +
                       " S2: (" + mControlPoint_2X + " ; " + mControlPoint_2Y + ") \n");
      Console.WriteLine(mVectorNormalX + "  <<vektor x \n" +
                        mVectorNormalY + "  <<vektor y \n" +
                       "P1(" + xSegmentPoints[0].X + " ; " + xSegmentPoints[0].Y + ") \n" +
                       "P2(" + xSegmentPoints[1].X + " ; " + xSegmentPoints[1].Y + ") \n");
    }

    internal static void SetCheckCirclePoint(cPoint xCircleCenter, int xSegmentPointX, int xSegmentPointY)
    {
      //fukcja ustawiająca int pCheckCirclePoint w zależności od tego w której ćwiartce znajduje się środek segmentu
      //xCircleCenter - 
      //xSegmentPointX - 
      //xSegmentPointY - 
      //pQuarterOfCoordinateSystem - 

      int xQuarterOfCoordinateSystem = new int();


      if (xCircleCenter.X <= xSegmentPointX && xCircleCenter.Y <= xSegmentPointY)
      {
        xQuarterOfCoordinateSystem = 1;
      }
      else if (xCircleCenter.X > xSegmentPointX && xCircleCenter.Y <= xSegmentPointY)
      {
        xQuarterOfCoordinateSystem = 2;
      }
      else if (xCircleCenter.X > xSegmentPointX && xCircleCenter.Y > xSegmentPointY)
      {
        xQuarterOfCoordinateSystem = 3;
      }
      else if (xCircleCenter.X <= xSegmentPointX && xCircleCenter.Y > xSegmentPointY)
      {
        xQuarterOfCoordinateSystem = 4;
      }
      else
      {
        Console.WriteLine("Błąd liczenia ćwiartki");
      }
      mCheckCirclePoint = xQuarterOfCoordinateSystem;
      Console.WriteLine("xQuarterOfCoordinateSystem : " + xQuarterOfCoordinateSystem);
      Console.WriteLine("xCircleCenter.x : " + xCircleCenter.X);
      Console.WriteLine("xCircleCenter.y : " + xCircleCenter.Y);
      Console.WriteLine("xSegmentPointX : " + xSegmentPointX);
      Console.WriteLine("xSegmentPointY : " + xSegmentPointY);
      Console.WriteLine("pCheckCirclePoint : " + mCheckCirclePoint);

      // return pQuarterOfCoordinateSystem;
    }

  }
}

