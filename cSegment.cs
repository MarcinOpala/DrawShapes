using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public enum SegmentIsIncludedEnum {                       //numerator opisujący w jakim poligonie jest zawarty
    Undefined = 0,                                          //nieokreślony
    FrameOutline = 1,                                       //tylko w wielokącie ramy
    Mullion = 2,                                            //w wielokącie słupka
 // MullionMullion = 3 ?????                                //w dwóch słupkach
  }

  public class cSegment {

    private SegmentIsIncludedEnum mCntSInclude;             //typ zawarcia w wielokącie
    private int mIndex;                                     //numer danego cSegmentu
    private bool mIsCurve;                                  //czy jest łuk
    private cPoint mPoint;                                  //punkt początkowy danego cSegmentu
    private cPolygon mPolygon_Parent;                       //poligon rodzica

    internal SegmentIsIncludedEnum CntSInclude { get { return mCntSInclude; } set { mCntSInclude = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }
    internal bool IsCurve { get { return mIsCurve; } set { mIsCurve = value; } }
    internal cPoint Point { get { return mPoint; } set { mPoint = value; } }
    internal cPolygon Polygon_Parent { get { return mPolygon_Parent; } }
    internal cSegment Segment_Next { get { return GetSegment_Next(); } }

    public cSegment() {

      mCntSInclude = SegmentIsIncludedEnum.Undefined;

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
      mCntSInclude = SegmentIsIncludedEnum.Undefined;
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

    internal void SetPointByVector(cVector xVector) {

      cSegment pSegment;

      pSegment = this;


      if (pSegment.Index == 1) {
        pSegment.Point.X += (float)xVector.X;
        pSegment.Point.Y += (float)xVector.Y;


      } else if (pSegment.Index == 2) {
        pSegment.Point.X -= (float)xVector.X;
        pSegment.Point.Y += (float)xVector.Y;

      } else if (pSegment.Index == 3) {
        pSegment.Point.X -= (float)xVector.X;
        pSegment.Point.Y -= (float)xVector.Y;

      } else {
        pSegment.Point.X += (float)xVector.X;
        pSegment.Point.Y -= (float)xVector.Y;

      }

    }

  }

}
