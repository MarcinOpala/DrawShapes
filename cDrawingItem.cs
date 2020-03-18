using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingItem {

    private Dictionary<int, cDrawingSegment> mDrawingSegments;

    internal Dictionary<int, cDrawingSegment> DrawingSegments { get { return mDrawingSegments; } set { mDrawingSegments = value; } }

    public cDrawingItem() {

      mDrawingSegments = new Dictionary<int, cDrawingSegment>();

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

