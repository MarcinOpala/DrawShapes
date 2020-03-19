using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawShape {

  public class cPolygonFactory {
    //klasa tworząca listy segmentów, do rysowania figur geometrycznych.
    // 1.Prostokąt - GetPolygon_Rect
    // 2.Wielokąt Foremny - GetPolygon_Regular

    public static cPolygon GetPolygon_Rect(int xWidth, int xHeight) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta

      cPoint pBasePt;
      cPolygon pPolygon_Rect;
      cSegment pSegment;

      pBasePt = new cPoint(0, 0);
      pPolygon_Rect = new cPolygon();

      pSegment = GetSegment(0, 0, pBasePt, 1);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(xWidth, 0, pBasePt, 2);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(xWidth, xHeight, pBasePt, 3);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(0, xHeight, pBasePt, 4);
      pPolygon_Rect.AddSegment(pSegment);

      pPolygon_Rect.ShowSegmentsList();

      return pPolygon_Rect;

    }

    public static cPolygon GetPolygon_Regular(int xRadius, double xAngle) {
      //funkcja dodająca segmenty do listy, z której powstaje wielokąt foremny
      //xRadius - promień koła w który jest wpisana figura
      //xAngle - kąt pomiędzy [punktem pPoint - środkiem koła xCircleCenter - punktem pPoint(z kolejnego segmentu)]

      cPoint pBasePt;                                       //punkt bazowy do rysowania figury
      cPoint pCircleCenter;                                 //środek koła
      double pCosTotalAngle;                                //cosunus kąta pAngleTemp
      double pSinTotalAngle;                                //sinus kąta pAngleTemp
      double pIdxCircleAngle;                               //indeks biegnący po okręgu
      int pNumber;                                          //numer danego segmentu
      cPolygon pPolygon_Regular;                            //polygon wieloboku
      cSegment pSegment;                                    //segment wieloboku
      double pTotalAngle;                                   //wartość kąta pomiędzy punktem pierwszego segmentu - środkiem koła - punktem kolejnego segmentu
      double pTotalAngleInRadius;                           //kąt pAngleTemp przedstawiony w radianach

      pPolygon_Regular = new cPolygon();
      pCircleCenter = new cPoint(0, 0);
      pBasePt = new cPoint(pCircleCenter.X + xRadius, pCircleCenter.Y + xRadius);
      pNumber = 0;

      //pętla dodająca kolejne segmenty
      for (pIdxCircleAngle = 0; pIdxCircleAngle <= (360 - Math.Abs(xAngle)); pIdxCircleAngle += Math.Abs(xAngle)) {
        pTotalAngle = 360 + (xAngle / 2) + (Math.Abs(xAngle)) * pNumber;
        pTotalAngleInRadius = (pTotalAngle * (Math.PI)) / 180;
        pCosTotalAngle = -Math.Cos(pTotalAngleInRadius);
        pSinTotalAngle = Math.Sin(pTotalAngleInRadius);

        pNumber++;

        pSegment = GetSegment((int)(xRadius * pSinTotalAngle), (int)(xRadius * pCosTotalAngle),
                   pBasePt, pNumber);

        pSegment.SetPolygon_Parent(pPolygon_Regular);

        pPolygon_Regular.AddSegment(pSegment);
       
      }

      pPolygon_Regular.ShowSegmentsList();                  //kontrola punktów: Consola output

      return pPolygon_Regular;

    }

    public static cSegment GetSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xNumber) {
      //funkcja zwracająca segment
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumber - numer segmentu

      cPoint pPoint;
      cSegment pSegment;
     
      pPoint = new cPoint(xPoint.X + xOffsetX, xPoint.Y + xOffsetY);
      pSegment = new cSegment(pPoint, xNumber, false, new cPolygon());

      return pSegment;

    }

  }

}
