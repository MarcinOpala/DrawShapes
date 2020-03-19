using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cSegment {

    private cPoint mPoint;                                  //punkt początkowy danego cSegmentu
    private int mNumber;                                    //numer danego cSegmentu
    private bool mIsCurve;                                  //czy jest łuk
    private cPolygon mPolygon_Parent;                       //poligon rodzica

    internal cPoint Point { get { return mPoint; } set { mPoint = value; } }
    internal int Number { get { return mNumber; } set { mNumber = value; } }
    internal cSegment Segment_Next { get { return GetSegment_Next(); } }
    internal cPolygon Polygon_Parent { get { return mPolygon_Parent; } }
    internal bool IsCurve { get { return mIsCurve; } set { mIsCurve = value; } }

    public cSegment() {

    }

    public cSegment(cPoint xPoint, int xNumber, bool xIsCurve, cPolygon xPolygon_Parent) {
      //xPointOfSegment - pierwszy punkt danego cSegmentu
      //xNumberOfSegment - numer danego cSegmentu

      mPoint = new cPoint(xPoint.X, xPoint.Y);
      mNumber = xNumber;
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

      int pNumber_Next;
      int pCountMax;

      pCountMax = mPolygon_Parent.Segments.Count;

      pNumber_Next = mNumber + 1;

      if (mNumber > pCountMax)
        pNumber_Next = 1;

      return mPolygon_Parent.GetSegmentByNumber(pNumber_Next);

    }

    internal void FillMeByObject(cSegment xSegment) {
      //funkcja wypełniająca podstawowe dane według wybranego boku
      //xSegment - bok bazowy

      mPoint = new cPoint(xSegment.Point.X, xSegment.Point.Y);
      mNumber = xSegment.Number;
      mIsCurve = xSegment.IsCurve;
      
      //zabraniamy kopiowania takich obiektów, trzeba przypisać je osobno
      //mPolygon_Parent = xSegment.Polygon_Parent;

    }

    internal cSegment Clone() {
      //funkcja zwracająca kopie boku (wypełnionionia tylko podstawowe pola)

      cSegment pSegment = new cSegment();

      pSegment.FillMeByObject(this);

      return pSegment;

    }

  }

}
