using System;
using System.Collections.Generic;
using System.Drawing;

namespace DrawShape {

  public class cPolygonFactory {
    //klasa tworząca listy segmentów, do rysowania figur geometrycznych.
    // 1.Prostokąt - GetPolygon_Rect
    // 2.Wielokąt Foremny - GetPolygon_Regular

    public static cPolygon GetPolygon_Rect(int xWidth, int xHeight, int xIndex) {
      //funkcja dodająca 4 cSegmenty do listy, z której powstaje prostokąt
      //xWidth - szerokość prostokąta
      //xHeight - wysokość prostokąta

      cPoint pBasePt;
      cPolygon pPolygon_Rect;
      cSegment pSegment;
      
      pBasePt = new cPoint(0, 0);
      pPolygon_Rect = new cPolygon(xIndex);

      pSegment = GetSegment(0, 0, pBasePt, 1, xIndex);
      pSegment.SetPolygon_Parent(pPolygon_Rect);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(xWidth, 0, pBasePt, 2, xIndex);
      pSegment.SetPolygon_Parent(pPolygon_Rect);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(xWidth, xHeight, pBasePt, 3, xIndex);
      pSegment.SetPolygon_Parent(pPolygon_Rect);
      pPolygon_Rect.AddSegment(pSegment);

      pSegment = GetSegment(0, xHeight, pBasePt, 4, xIndex);
      pSegment.SetPolygon_Parent(pPolygon_Rect);
      pPolygon_Rect.AddSegment(pSegment);

      pPolygon_Rect.ShowSegmentsList();

      return pPolygon_Rect;

    }

    public static cPolygon GetPolygon_Regular(int xDiameter, int xSegementsQuantity, int xIndex) {
      //funkcja dodająca segmenty do listy, z której powstaje wielokąt foremny
      //xDiameter - średnica okręgu w który jest wielokąt
      //xSegementsQuantity - liczba boków wielokąta
      //xIndex - numer wielokąta

      cPoint pBasePt;                                       //punkt bazowy do rysowania figury
      cPoint pCircleCenter;                                 //środek koła
      double pCosTotalAngle, pSinTotalAngle;                //Cos, Sin kąta pAngleTemp
      double pIdxCircleAngle;                               //indeks biegnący po okręgu
      int pIndex_segm;                                      //numer danego boku
      cPolygon pPolygon_Regular;                            //wielokąt
      cSegment pSegment;                                    //bok wielokąta
      double pTotalAngle;                                   //wartość kąta pomiędzy punktem pierwszego segmentu - środkiem koła - punktem kolejnego segmentu
      double pTotalAngleInRadius;                           //kąt pAngleTemp przedstawiony w radianach

      double pBaseAngle;
      int pRadius;

      pBaseAngle = 360 / xSegementsQuantity;            //kąt pomiędzy: punktem boku 1 - środekiem okręgu - punktem boku 2
      pRadius = xDiameter / 2;

      pPolygon_Regular = new cPolygon(xIndex);
      pCircleCenter = new cPoint(0, 0);
      pBasePt = new cPoint(pCircleCenter.X + pRadius, pCircleCenter.Y + pRadius);
      pIndex_segm = 0;

      //pętla dodająca kolejne segmenty
      for (pIdxCircleAngle = 0; pIdxCircleAngle <= (360 - pBaseAngle); pIdxCircleAngle += pBaseAngle) {
        pTotalAngle = 360 + (- pBaseAngle / 2) + pBaseAngle * pIndex_segm;
        pTotalAngleInRadius = (pTotalAngle * (Math.PI)) / 180;
        pCosTotalAngle = -Math.Cos(pTotalAngleInRadius);
        pSinTotalAngle = Math.Sin(pTotalAngleInRadius);

        pIndex_segm++;

        pSegment = GetSegment((int)(pRadius * pSinTotalAngle), (int)(pRadius * pCosTotalAngle),
                   pBasePt, pIndex_segm, xIndex);

        pSegment.SetPolygon_Parent(pPolygon_Regular);

        pPolygon_Regular.AddSegment(pSegment);
       
      }

      pPolygon_Regular.ShowSegmentsList();                  //kontrola punktów: Consola output

      return pPolygon_Regular;

    }

    public static cSegment GetSegment(int xOffsetX, int xOffsetY, cPoint xPoint, int xIndex_Segm, int xIndex_Poly) {
      //funkcja zwracająca nowy bok
      //xOffsetX - przesunięcie względem osi X
      //xOffsetY - przesunięcie względem osi Y
      //xPoint - współrzędne punktu bazowego
      //xNumber - numer segmentu

      cPoint pPoint;
      cSegment pSegment;
     
      pPoint = new cPoint(xPoint.X + xOffsetX, xPoint.Y + xOffsetY);
      pSegment = new cSegment(pPoint, xIndex_Segm, false, new cPolygon(xIndex_Poly));

      return pSegment;

    }

  }

}
