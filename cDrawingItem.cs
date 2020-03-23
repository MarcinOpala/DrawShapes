using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingItem {

    private Dictionary<int, cDrawingSegment> mDrawingSegments;
    private int mIndex;

    internal Dictionary<int, cDrawingSegment> DrawingSegments { get { return mDrawingSegments; } set { mDrawingSegments = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }


    public cDrawingItem() {

      mDrawingSegments = new Dictionary<int, cDrawingSegment>();
     

    }

    public cDrawingItem(int xIndex) {

      mDrawingSegments = new Dictionary<int, cDrawingSegment>();
      mIndex = xIndex;

    }

    internal void AddSegment(cDrawingSegment xDrawingSegments) {
      //funkcja dodająca nowy segment do listy
      //xDrawingSegments - wybrany segment
      //xNumber - numer segmentu

      mDrawingSegments.Add(xDrawingSegments.Index, xDrawingSegments);

    }

    internal cDrawingSegment GetSegmentByNumer(int xNumber) {
      //funkcja zwracająca segment
      //xSegmentNumber - numer segmentu

      int pSegmentNumber;
      int pCountMax;

      pCountMax = mDrawingSegments.Count;

      pSegmentNumber = xNumber;

      if (xNumber > pCountMax)
        pSegmentNumber = 1;

      return mDrawingSegments[pSegmentNumber];

    }

  }

}

