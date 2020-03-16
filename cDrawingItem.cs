using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingItem {

    private List<cDrawingSegment> mDrawingSegments;

    internal List<cDrawingSegment> DrawingSegments { get { return mDrawingSegments; } set { mDrawingSegments = value; } }

    public cDrawingItem() {

      mDrawingSegments = new List<cDrawingSegment>();

    }

    internal void AddSegment(cDrawingSegment xDrawingSegments, int xNumber) {
      //funkcja dodająca nowy segment do listy
      //xDrawingSegments - wybrany segment
      //xNumber - numer segmentu

      mDrawingSegments.Insert(0, xDrawingSegments);

    }

    internal cDrawingSegment GetSegmentByNumer(int xNumber) {
      //funkcja zwracająca segment
      //xSegmentNumber - numer segmentu

      int pSegmentNumber;
      int pCountMax;

      pCountMax = mDrawingSegments.Count - 1;

      pSegmentNumber = xNumber;

      if (xNumber > pCountMax)
        pSegmentNumber = 0;


      return mDrawingSegments[pSegmentNumber];

    }
  }
}

