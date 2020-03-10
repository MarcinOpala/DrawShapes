using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawShape {

  public class cDrawingAdapter {

                                                            //                                                           3|4 
    private static int mCheckCirclePoint;                   //nr ćwiartki pCenterPointOfSegment liczona według schematu: 2|1
    private static cPoint mCircleCenter;
   
    internal static cPoint CircleCenter { get { return mCircleCenter; } set { mCircleCenter = value; } }

    public void DrawPolygon(cPolygon xPolygon, PaintEventArgs e) {
      //funkcja rysująca dowolny wielobok
      //xPolygon - baza punktów
     
      Pen pBluePen;                                         //kolor mazaka
      PointF[] pPointsLine;                                 //punkty tworzące linie
      int pCount;                                           //liczba boków figury
      int pIndexNext;                                            //następny index segmentu
      int pIndex;

      pBluePen = new Pen(Color.Blue, 3);
      pPointsLine = new PointF[2];
      pCount = xPolygon.Segments.Count;

      //pętla rysująca linię składającą się z dwóch punktów
      for (pIndex = 0; pIndex <= (pCount - 1); pIndex++) {

        pIndexNext = pIndex + 1;

        if (pIndex == (pCount - 1)) {                            //następcą ostatniego punktu jest punkt początkowy
          pIndexNext = 0;
        }

        cSegment pSegment = xPolygon.GetSegmentByNumer(pIndex);
        cSegment pSegment_Next = xPolygon.GetSegmentByNumer(pIndexNext);
        //wrzucić do xPolygon^^


        if (!pSegment.IsCurve) {                        //sprawdzenie lini: prosta, czy krzywa
          pPointsLine[0] = new PointF(pSegment.Point.X,
                                      pSegment.Point.Y);
          pPointsLine[1] = new PointF(pSegment_Next.Point.X,
                                      pSegment_Next.Point.Y);
          e.Graphics.DrawPolygon(pBluePen, pPointsLine);
        } else {
         // DrawBezierCurve(pSegment, pSegment_Next, e);
         //[MO 10.03.2020] funkcja chwilowo wyłączona do poprawnej kompilacji
        }

      }

    }
    //[MO 10.03.2020] funkcja chwilowo wyłączona do poprawnej kompilacji
    /*internal static void DrawBezierCurve(cSegment xSegment, cSegment xSegment_Next, PaintEventArgs e) {
      //funkcja rysująca łuk po wybraniu boku
      //xNumber - numer segmentu
      //xSegments - nazwa listy segmentów

      float pCenterX;                                       //punkt X środka segmentu
      float pCenterY;                                       //punkt Y środka segmentu
      float pControl_1X;                                    //punkt X punktu kotrolnego 1 do rysowania łuku 1:4 odcinka segmentu
      float pControl_1Y;                                    //punkt Y punktu kotrolnego 1 do rysowania łuku
      float pControl_2X;                                    //punkt X punktu kotrolnego 2 do rysowania łuku 
      float pControl_2Y;                                    //punkt Y punktu kotrolnego 2 do rysowania łuku
      double pVectNormalX;                                  //X wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych
      double pVectNormalY;                                  //Y wektora prostopadłego do segmenty, do przesunięcia punktów kontrolnych

      // f(x)=0
      if ((xSegments[xNumber].Point.X - xSegments[xNumber+1].Point.X == 0)) {
        pVectNormalX = Math.Abs((xSegments[xNumber + 1].Point.Y - xSegments[xNumber].Point.Y) / 4);
        pVectNormalY = 0;
      }
      // f(y)=0
      else if ((xSegments[xNumber].Point.Y - xSegments[xNumber + 1].Point.Y == 0)) {
        pVectNormalX = 0;
        pVectNormalY = Math.Abs((xSegments[xNumber + 1].Point.X - xSegments[xNumber].Point.X) / 4);
      }
      // f(y)=ax+b
      else {
        int pIncreaseVector = 50;
        pVectNormalX = Math.Abs((xSegments[xNumber].Point.Y - xSegments[xNumber + 1].Point.Y) /
                          (xSegments[xNumber].Point.X - xSegments[xNumber + 1].Point.X)) * pIncreaseVector;
        pVectNormalY = pIncreaseVector;
      }
      pCenterX = (xSegments[xNumber].Point.X + xSegments[xNumber+1].Point.X) / 2;
      pCenterY = (xSegments[xNumber].Point.Y + xSegments[xNumber+1].Point.Y) / 2;

      Console.WriteLine(" S: (" + pCenterX + " ; " + pCenterY + ") ");
      SetCheckCirclePoint(mCircleCenter, (int)pCenterX, (int)pCenterY);

      //przesunięcie środka segmentu o wektor 
      //w zależności od ćwiartki w której się znajduję środek koła,
      //na którym opisana jest figura
      if (mCheckCirclePoint == 1) {
        pCenterX = pCenterX + (float)pVectNormalX;
        pCenterY = pCenterY + (float)pVectNormalY;
      } else if (mCheckCirclePoint == 2) {
        pCenterX = pCenterX - (float)pVectNormalX;
        pCenterY = pCenterY + (float)pVectNormalY;
      } else if (mCheckCirclePoint == 3) {
        pCenterX = pCenterX - (float)pVectNormalX;
        pCenterY = pCenterY - (float)pVectNormalY;
      } else if (mCheckCirclePoint == 4) {
        pCenterX = pCenterX + (float)pVectNormalX;
        pCenterY = pCenterY - (float)pVectNormalY;
      } else {
        pCenterX = pCenterX + (float)pVectNormalX;
        pCenterY = pCenterY - (float)pVectNormalY;
      }

      pControl_1X = (xSegments[xNumber].Point.X + pCenterX) / 2;
      pControl_1Y = (xSegments[xNumber].Point.Y + pCenterY) / 2;
      pControl_2X = (pCenterX + xSegments[xNumber+1].Point.X) / 2;
      pControl_2Y = (pCenterY + xSegments[xNumber+1].Point.Y) / 2;

      PointF start = new PointF(xSegments[xNumber].Point.X, xSegments[xNumber].Point.Y);
      PointF pControlPoint1 = new PointF(pControl_1X, pControl_1Y);
      PointF pControlPoint2 = new PointF(pControl_2X, pControl_2Y);
      PointF end = new PointF(xSegments[xNumber+1].Point.X, xSegments[xNumber+1].Point.Y);
      Pen pBluePen = new Pen(Color.Blue, 3);

      e.Graphics.DrawBezier(pBluePen, start, pControlPoint1, pControlPoint2, end);

    }*/

    internal static void SetCheckCirclePoint(cPoint xCircleCenter, int xSegmentPointX, int xSegmentPointY) {
      //fukcja ustawiająca int pCheckCirclePoint w zależności od tego w której ćwiartce znajduje się środek segmentu
      //xCircleCenter - 
      //xSegmentPointX - 
      //xSegmentPointY - 
      
      int pQuarterOfCoordinateSystem = new int();

      if (xCircleCenter.X <= xSegmentPointX && xCircleCenter.Y <= xSegmentPointY) {
        pQuarterOfCoordinateSystem = 1;
      } else if (xCircleCenter.X > xSegmentPointX && xCircleCenter.Y <= xSegmentPointY) {
        pQuarterOfCoordinateSystem = 2;
      } else if (xCircleCenter.X > xSegmentPointX && xCircleCenter.Y > xSegmentPointY) {
        pQuarterOfCoordinateSystem = 3;
      } else if (xCircleCenter.X <= xSegmentPointX && xCircleCenter.Y > xSegmentPointY) {
        pQuarterOfCoordinateSystem = 4;
      } else {
        Console.WriteLine("Błąd liczenia ćwiartki");
      }
      
      mCheckCirclePoint = pQuarterOfCoordinateSystem;

    }
    
  }
          
}


