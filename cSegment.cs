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
    internal cPolygon Polygon_Parent { get { return mPolygon_Parent; } }
    internal cSegment Segment_Next { get { return GetSegment_Next(); } }

    public cSegment() {

    

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

  }

}
