using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public enum DrawingItemFillingEnum {                       //numerator funkcjonalności wielokąta
    Undefined = 0,                                           //nieokreślony
    IsFilled = 1,                                            //AItem ma być wypełniony kolorem
  }

  public class cDrawingItem {

    private DrawingItemFillingEnum mCntDIF;                //informacja o tym, czy element jest wypełniony
    private Dictionary<int, cDrawingSegment> mDrawingSegments;
    private int mIndex;

    internal DrawingItemFillingEnum CntDIF { get { return mCntDIF; } set { mCntDIF = value; } }
    internal Dictionary<int, cDrawingSegment> DrawingSegments { get { return mDrawingSegments; } set { mDrawingSegments = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }


    public cDrawingItem() {

      mDrawingSegments = new Dictionary<int, cDrawingSegment>();

    }

    public cDrawingItem(int xIndex) {
      //xIndex - numer drawing itemu

      mDrawingSegments = new Dictionary<int, cDrawingSegment>();
      mIndex = xIndex;

    }

    internal void AddSegment(cDrawingSegment xDrawingSegments) {
      //funkcja dodająca nowy segment do listy
      //xDrawingSegments - wybrany segment

      mDrawingSegments.Add(xDrawingSegments.Index, xDrawingSegments);

    }

    internal cDrawingSegment GetSegmentByIndex(int xIndex) {
      //funkcja zwracająca segment po indeksie
      //xIndex - numer boku

      int pIndexSegment;
      int pCountMax;

      pCountMax = mDrawingSegments.Count;

      pIndexSegment = xIndex;

      if (xIndex > pCountMax)
        pIndexSegment = 1;

      return mDrawingSegments[pIndexSegment];

    }

  }

}

