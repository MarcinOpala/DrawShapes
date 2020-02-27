using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawShape {
  public class cPolygon  {

    public PointF[] pSegmentPoints = new PointF[2];         //inicjacja pomocniczej tablicy - dwa punkty każdego segmentu
    public double pVectorNormalX;                           //X wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych
    public double pVectorNormalY;                           //Y wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych
    public float pCenterPointOfSegmentX;                    //X środka segmentu
    public float pCenterPointOfSegmentY;                    //Y środka segmentu
    public float pControlPoint_1X;                          //X punktu kotrolnego 1 do rysowania łuku 1:4 odcinka segmentu
    public float pControlPoint_1Y;                          //Y punktu kotrolnego 1 do rysowania łuku
    public float pControlPoint_2X;                          //X punktu kotrolnego 2 do rysowania łuku 
    public float pControlPoint_2Y;                          //Y punktu kotrolnego 2 do rysowania łuku
    public int pIsSelected;                                 //nr wybranego boku do przekształcenia w łuk
    public int pNumberOfSides;                              //liczba boków figury
    public int pCheckCirclePoint;                           //nr ćwiartki pCenterPointOfSegment liczona według schematu: 3|4
                                                            //                                                           2|1      
    public void DrawRectangle(PaintEventArgs e) {
      //funkcja rysująca prostokąt z listy punktów w klasnie cSegment
      
      pCheckCirclePoint = 3; 
      cPolygonFactory.GetNewSegmentList(cPolygonFactory.pRectangleSegmentList);
      DrawWithOutSelectedSide(e, pSegmentPoints, cPolygonFactory.pRectangleSegmentList, 3);
      DrawBezierPointF(e, pSegmentPoints);
    }

    public void DrawRegularPolygon(PaintEventArgs e) {
      //funkcja rysująca wielokąt foremny z listy punktów w klasnie cSegment
      
      pCheckCirclePoint = 3;
      cPolygonFactory.GetNewSegmentList(cPolygonFactory.pRegularPolygonCSegmentList);
      DrawWithOutSelectedSide(e, pSegmentPoints, cPolygonFactory.pRegularPolygonCSegmentList, pIsSelected);
      DrawBezierPointF(e, pSegmentPoints);
    }

    public void DrawWithOutSelectedSide(PaintEventArgs e, PointF[] xSegmentPoints, List<cSegment> xSegmentsList, int xSelected) {
      //funkcja rysująca figurę bez zaznaczonego boku 
      //xSegmentPoints - tablica 2 punktów, tworzących bok
      //xSegmentsList - lista wszystkich segmentów wybranej figury
      //xSelected - wybranie boku do "złukowania"

      Pen pBluePen = new Pen(Color.Blue, 3);
      pNumberOfSides = xSegmentsList.Count;
     
      //rysuję figurę do pIsSelected
      for (int i = 0; i <= xSelected - 2; i++) {
        if (i == (pNumberOfSides - 1)) {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].pPointOfSegment.x,
                                         xSegmentsList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentsList[0].pPointOfSegment.x,
                                         xSegmentsList[0].pPointOfSegment.y);
        }
        else {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].pPointOfSegment.x,
                                         xSegmentsList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentsList[i + 1].pPointOfSegment.x,
                                         xSegmentsList[i + 1].pPointOfSegment.y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      //rysuję figurę od pIsSelected
      for (int i = xSelected; i <= pNumberOfSides - 1; i++) {
        if (i == (pNumberOfSides - 1)) {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].pPointOfSegment.x,
                                         xSegmentsList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentsList[0].pPointOfSegment.x,
                                         xSegmentsList[0].pPointOfSegment.y);
        }
        else {
          xSegmentPoints[0] = new PointF(xSegmentsList[i].pPointOfSegment.x,
                                         xSegmentsList[i].pPointOfSegment.y);
          xSegmentPoints[1] = new PointF(xSegmentsList[i + 1].pPointOfSegment.x,
                                         xSegmentsList[i + 1].pPointOfSegment.y);
        }
        e.Graphics.DrawPolygon(pBluePen, xSegmentPoints);
      }

      // utworzenie dwóch dodatkowych punktów potrzebnych do narysowania łuku
      xSegmentPoints[0] = new PointF(xSegmentsList[xSelected - 1].pPointOfSegment.x,
                                     xSegmentsList[xSelected - 1].pPointOfSegment.y);
      if (xSelected == pNumberOfSides) xSelected = 0;
      xSegmentPoints[1] = new PointF(xSegmentsList[xSelected].pPointOfSegment.x,
                                     xSegmentsList[xSelected].pPointOfSegment.y);
    }

    private void DrawBezierPointF(PaintEventArgs e, PointF[] xSegmentPoints) {
      //funkcja rysująca łuk po wybraniu boku
      //xSegmentPoints - lista 2 punktów do narysowania boku

      // f(x)=0
      if ((xSegmentPoints[0].X - xSegmentPoints[1].X) == 0) {
        pVectorNormalX = Math.Abs((xSegmentPoints[1].Y - xSegmentPoints[0].Y) / 4);
        pVectorNormalY = 0;
      }
      // f(y)=0
      else if ((xSegmentPoints[0].Y - xSegmentPoints[1].Y) == 0) {
        pVectorNormalX = 0;
        pVectorNormalY = Math.Abs((xSegmentPoints[1].X - xSegmentPoints[0].X) / 4);
      }
      // f(y)=ax+b
      else {
        int pIncreaseVector = 50;
        pVectorNormalX = Math.Abs((xSegmentPoints[0].Y - xSegmentPoints[1].Y) /
                                  (xSegmentPoints[0].X - xSegmentPoints[1].X)) * pIncreaseVector;
        pVectorNormalY = pIncreaseVector;
      }

      pCenterPointOfSegmentX = (xSegmentPoints[0].X + xSegmentPoints[1].X) / 2;
      pCenterPointOfSegmentY = (xSegmentPoints[0].Y + xSegmentPoints[1].Y) / 2;
      Console.WriteLine(" S: (" + pCenterPointOfSegmentX + " ; " + pCenterPointOfSegmentY + ") ");

      //przesunięcie środka segmentu o wektor 
      //w zależności od ćwiartki w której się znajduję środek koła,
      //na którym opisana jest figura
      if (pCheckCirclePoint == 1) {
        pCenterPointOfSegmentX = pCenterPointOfSegmentX + (float)pVectorNormalX;
        pCenterPointOfSegmentY = pCenterPointOfSegmentY + (float)pVectorNormalY;
      }
      else if (pCheckCirclePoint == 2) {
        pCenterPointOfSegmentX = pCenterPointOfSegmentX - (float)pVectorNormalX;
        pCenterPointOfSegmentY = pCenterPointOfSegmentY + (float)pVectorNormalY;
      }
      else if (pCheckCirclePoint == 3) {
        pCenterPointOfSegmentX = pCenterPointOfSegmentX - (float)pVectorNormalX;
        pCenterPointOfSegmentY = pCenterPointOfSegmentY - (float)pVectorNormalY;
      }
      else if (pCheckCirclePoint == 4) {
        pCenterPointOfSegmentX = pCenterPointOfSegmentX + (float)pVectorNormalX;
        pCenterPointOfSegmentY = pCenterPointOfSegmentY - (float)pVectorNormalY;
      }
      else {
        pCenterPointOfSegmentX = pCenterPointOfSegmentX + (float)pVectorNormalX;
        pCenterPointOfSegmentY = pCenterPointOfSegmentY - (float)pVectorNormalY;
      }

      pControlPoint_1X = (xSegmentPoints[0].X + pCenterPointOfSegmentX) / 2;
      pControlPoint_1Y = (xSegmentPoints[0].Y + pCenterPointOfSegmentY) / 2;
      pControlPoint_2X = (pCenterPointOfSegmentX + xSegmentPoints[1].X) / 2;
      pControlPoint_2Y = (pCenterPointOfSegmentY + xSegmentPoints[1].Y) / 2;

      PointF start = new PointF(xSegmentPoints[0].X, xSegmentPoints[0].Y);
      PointF pControlPoint1 = new PointF(pControlPoint_1X, pControlPoint_1Y);
      PointF pControlPoint2 = new PointF(pControlPoint_2X, pControlPoint_2Y);
      PointF end = new PointF(xSegmentPoints[1].X, xSegmentPoints[1].Y);
      Pen pBluePen = new Pen(Color.Blue, 3);
      e.Graphics.DrawBezier(pBluePen, start, pControlPoint1, pControlPoint2, end);

      Console.WriteLine(" S: (" + pCenterPointOfSegmentX + " ; " + pCenterPointOfSegmentY + ") \n" +
                       " S1: (" + pControlPoint_1X + " ; " + pControlPoint_1Y + ") \n" +
                       " S2: (" + pControlPoint_2X + " ; " + pControlPoint_2Y + ") \n");
      Console.WriteLine(pVectorNormalX + "  <<vektor x \n" +
                        pVectorNormalY + "  <<vektor y \n" +
                       "P1(" + xSegmentPoints[0].X + " ; " + xSegmentPoints[0].Y + ") \n" +
                       "P2(" + xSegmentPoints[1].X + " ; " + xSegmentPoints[1].Y + ") \n");
    }
  }
}
