using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cSegment {

    private int mIndex;                                     //numer danego cSegmentu
    private bool mIsCurve;                                  //czy jest łuk
    private cPoint mPoint;                                  //punkt początkowy danego cSegmentu
    private cPolygon mPolygon_Parent;                       //poligon rodzica

    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal bool IsCurve { get { return mIsCurve; } set { mIsCurve = value; } }
    internal cPoint Point { get { return mPoint; } set { mPoint = value; } }
    internal cPolygon Polygon_Parent { get { return mPolygon_Parent; } set { mPolygon_Parent = value; } }
    internal cSegment Segment_Next { get { return GetSegment_Next(); } }
    internal cSegment Segment_Before { get { return GetSegment_Before(); } }

    public cSegment() {

      mPoint = new cPoint();

    }


    public cSegment(int xIndex) {

      mIndex = xIndex;
      mPoint = new cPoint();

    }

    public cSegment(cPoint xPoint, int xIndex) {

      mIndex = xIndex;
      mPoint = new cPoint(xPoint.X, xPoint.Y);

    }

    public cSegment(cPoint xPoint, int xIndex, bool xIsCurve, cPolygon xPolygon_Parent) {
      //xPoint - pierwszy punkt danego cSegmentu
      //xIndex - numer danego cSegmentu
      //xIsCurve - ustawienie rodzaju krzywej
      //xPolygon_Parent - ustawienie rodzica

      mPoint = new cPoint(xPoint.X, xPoint.Y);
      mIndex = xIndex;
      mIsCurve = xIsCurve;
      mPolygon_Parent = xPolygon_Parent;
      
    }

    internal void SetPolygon_Parent(cPolygon xPolygon) {
      //funkcja przypisująca poligon do którego należy bok
      //xPolygon - poligon rodzica

      mPolygon_Parent = xPolygon;

    }

    private cSegment GetSegment_Next() {
      //funkja zwracjąca następny bok

      int pIndex_Next;
      int pCountMax;

      pCountMax = mPolygon_Parent.Segments.Count;

      pIndex_Next = mIndex + 1;

      if (mIndex > pCountMax)
        pIndex_Next = 1;

      return mPolygon_Parent.GetSegmentByIndex(pIndex_Next);

    }

    private cSegment GetSegment_Before() {
      //funkja zwracjąca następny bok

      int pIndex_Before;
      int pCountMax;

      pCountMax = mPolygon_Parent.Segments.Count;

      pIndex_Before = mIndex - 1;

      if (mIndex == 1)
        pIndex_Before = pCountMax;


      return mPolygon_Parent.GetSegmentByIndex(pIndex_Before);

    }

    internal void FillMeByObject(cSegment xSegment) {
      //funkcja wypełniająca podstawowe dane według wybranego boku
      //xSegment - bok bazowy

      mPoint = new cPoint(xSegment.Point.X, xSegment.Point.Y);
      mIndex = xSegment.Index;
      mIsCurve = xSegment.IsCurve;
      
      //zabraniamy kopiowania takich obiektów, trzeba przypisać je osobno
      //mPolygon_Parent = xSegment.Polygon_Parent;

    }

    internal cSegment Clone() {
      //funkcja zwracająca kopie boku (wypełnionionia tylko podstawowe pola)

      cSegment pSegment;

      pSegment = new cSegment();

      pSegment.FillMeByObject(this);

      return pSegment;

    }

    internal void MovePointInwardsPolygonByVector(cVector xVector) {
      //funkcja zmieniająca położenie punku
      //UWAGA: tylko dla prostokątów
      //xVector - wektor przesunięcia

      if (mIndex == 1) {
        mPoint.X += (float)xVector.X;
        mPoint.Y += (float)xVector.Y;

      } else if (mIndex == 2) {
        mPoint.X -= (float)xVector.X;
        mPoint.Y += (float)xVector.Y;

      } else if (mIndex == 3) {
        mPoint.X -= (float)xVector.X;
        mPoint.Y -= (float)xVector.Y;

      } else {
        mPoint.X += (float)xVector.X;
        mPoint.Y -= (float)xVector.Y;

      }

    }

    internal void CalculatePointOffset(cSegment xSegment, int xWidth_Profile, int xWidth_Mullion, cStraightLine xStraightLine, out float xPtX_Offset, out float xPtY_Offset) {
      //funkcja zwracająca wartość przesunięcia punktów. 
      //działanie: pokrywający się z segment_next vektor (o długości przekątnej profilu) obracamy o kąt przy podstawie profilu
      //xSegment - bazowy segment
      //xWidth_Profile - szerokość profilu
      //xPtX_Offset - końcowe przesunięcie X
      //xPtY_Offset - końcowe przesunięcie Y

      double pAlfa, pCosAlfa, pSinAlfa, pAlfaInRadius;
      double pBeta, pSinBeta, pCosBeta, pBetaInRadius;
      double pDiagonalSize;
      double pPt_X, pPt_Y;
      cVector pVector_SegNext, pVector_OX;
      cStraightLine pStraightLine_1, pStraightLine_2;
      double pBeta2;

      //nowy wektor pokrywający się z następnym bokiem
      pVector_SegNext = new cVector(xSegment, xSegment.Segment_Next);

      //nowy wektor pomocniczy pokrywający się z osią OX
      pVector_OX = new cVector(100, 0);

      //kąt pomiędzy wektorami
      pCosAlfa = cVector.CosAlfa(pVector_OX, pVector_SegNext);

      //ustawienie kąta Alfa
      if (pCosAlfa >= 0 && pVector_SegNext.Vector.Y >= 0) {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa < 0 && pVector_SegNext.Vector.Y > 0) {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa <= 0 && pVector_SegNext.Vector.Y <= 0) {
        pAlfa = 180 + (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else if (pCosAlfa >= 0 && pVector_SegNext.Vector.Y < 0) {
        pAlfa = 180 + (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      } else {
        pAlfa = (Math.Acos(pCosAlfa)) * 180 / Math.PI;

      }

      //obliczanie sin, cos 
      pAlfaInRadius = (pAlfa * (Math.PI)) / 180;
      pSinAlfa = Math.Sin(pAlfaInRadius);

      pStraightLine_1 = new cStraightLine(xSegment);
      //pStraightLine_2 = new cStraightLine(xStraightLine);

      pBeta = (180 - pStraightLine_1.Get_Angle(xStraightLine)) / 2;


      pBeta2 = (180 - pStraightLine_1.Get_Angle(xStraightLine));



      double pBeta2InRadius, pSinBeta2, pCosBeta2;

      pBeta2InRadius = (pBeta2 * (Math.PI)) / 180;
      pSinBeta2 = Math.Sin(pBeta2InRadius);
      pCosBeta2 = Math.Cos(pBeta2InRadius);



      // pBeta = (180 - (360 / xSegment.Polygon_Parent.Segments.Count)) / 2;
      pBetaInRadius = (pBeta * (Math.PI)) / 180;
      pSinBeta = Math.Sin(pBetaInRadius);
      pCosBeta = Math.Cos(pBetaInRadius);

      cVector pVector_1, pVector_2;
      cVector pC_Vector;

      pVector_1 = new cVector(xWidth_Profile / pSinBeta2, 0);    // wektor przesunięcia C tego boku
      pVector_2 = new cVector(0, xWidth_Mullion / pSinBeta2);    // wektor przesunięcia C poprzedniego boku

      pC_Vector = pVector_1.AddVectors(pVector_1, pVector_2);

      pDiagonalSize = pC_Vector.Get_VectorLength();
      //pDiagonalSize = xWidth_Profile / Math.Sin(pBetaInRadius);    //długość przekątnej profilu

      pPt_X = (pDiagonalSize * pCosAlfa);                          //pośrednie wartości przesunięcia
      pPt_Y = (pDiagonalSize * pSinAlfa);

      xPtX_Offset = (float)(pPt_X * pCosBeta2 - pPt_Y * pSinBeta2);  //obliczenie przesunięcia potrzebnego do wygenerowania pozycji punktu
      xPtY_Offset = (float)(pPt_X * pSinBeta2 + pPt_Y * pCosBeta2);

    }

  }

}
