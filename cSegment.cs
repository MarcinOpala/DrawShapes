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

    public cSegment(cPoint xPoint, int xIndex, cPolygon xPolygon_Parent) {
      //xPoint - pierwszy punkt danego cSegmentu
      //xIndex - numer danego cSegmentu
      //xPolygon_Parent - ustawienie rodzica

      mPoint = new cPoint(xPoint.X, xPoint.Y);
      mIndex = xIndex;
      mPolygon_Parent = xPolygon_Parent;

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

  }

}
