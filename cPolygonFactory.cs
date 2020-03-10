using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawShape {

  public class cPolygonFactory {
    //klasa tworząca listy segmentów, do rysowania figur geometrycznych.
    // 1.Prostokąt - GetPolygon_Rect
    // 2.Wielokąt Foremny - GetPolygon_Regular

    

    public static cPolygon GetPolygon_Rect(cPoint xPoint, int xWidth, int xHeight) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xPoint - współrzędne punktu bazowego
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta

      cPolygon pPolygon_Rect;
      cSegment pSegment;

      pPolygon_Rect = new cPolygon();

      pSegment = GetSegment(0, 0, xPoint, 1);
      pPolygon_Rect.AddSegment(pSegment, 0);

      pSegment = GetSegment(xWidth, 0, xPoint, 2);
      pPolygon_Rect.AddSegment(pSegment, 1);

      pSegment = GetSegment(xWidth, -xHeight, xPoint, 3);
      pPolygon_Rect.AddSegment(pSegment, 2);

      pSegment = GetSegment(0, -xHeight, xPoint, 4);
      pPolygon_Rect.AddSegment(pSegment, 3);

      pPolygon_Rect.ShowSegmentsList();

      return pPolygon_Rect;

    }
    
    public static cPolygon GetPolygon_Regular(cPoint xCircleCenter, int xRadius, double xAngle) {
      //funkcja dodająca segmenty do listy, z której powstaje wielokąt foremny
      //xCircleCenter - środek koła, na którym wpisana jest figura
      //xRadius - promień koła w który jest wpisana figura
      //xAngle - kąt pomiędzy [punktem pPoint - środkiem koła xCircleCenter - punktem pPoint(z kolejnego segmentu)]

      double pAngleTemp;                                    //wartość kąta pomiędzy punktem pierwszego segmentu - środkiem koła - punktem kolejnego segmentu
      double pAngleTotal;                                   //kąt pAngleTemp przedstawiony w radianach
      double pCosinusOfAngleTotal;                          //cosunus kąta pAngleTemp
      int pNumber;                                          //numer danego segmentu
      cPolygon pPolygon_Regular;
      cSegment pSegment;
      double pSinusOfAngleTotal;                            //sinus kąta pAngleTemp
      double pTotalSegments;                                //liczba wszystkich boków figury
      double pAngleIncremental;

      pNumber = 0;                                          
      pTotalSegments = 360 / Math.Abs(xAngle);
      pPolygon_Regular = new cPolygon();

      for (pAngleIncremental = 0; pAngleIncremental <= 360 - Math.Abs(xAngle); pAngleIncremental += Math.Abs(xAngle)) {
        pAngleTemp = xAngle / 2 + (Math.Abs(xAngle)) * pNumber;
        pAngleTotal = (pAngleTemp * (Math.PI)) / 180;
        pCosinusOfAngleTotal = Math.Cos(pAngleTotal);
        pSinusOfAngleTotal = Math.Sin(pAngleTotal);
        pNumber++;

        pSegment = GetSegment((int)(xRadius * pSinusOfAngleTotal), (int)(xRadius * pCosinusOfAngleTotal),
                   xCircleCenter, pNumber);

        pPolygon_Regular.AddSegment(pSegment, pNumber-1);
      }

      return pPolygon_Regular;

    }

    public  static cSegment GetSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xNumber) {
      //funkcja zwracająca segment
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumber - numer segmentu
      
      cPoint pPoint;
      PointF pPointHelper;
      cSegment pSegment;

      pPointHelper = new PointF(xPoint.X + xOffsetX, xPoint.Y + xOffsetY);
      pPoint = new cPoint(pPointHelper);
      pSegment = new cSegment(pPoint, xNumber);

      return pSegment;

    }

  }
}
