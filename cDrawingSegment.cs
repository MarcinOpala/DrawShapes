using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingSegment {

    private cSegment mSegment;
    private int mIndex;

    internal cSegment Segment { get { return mSegment; } set { mSegment = value; } }
    internal int Index { get { return mIndex; } set { mIndex = value; } }

    public cDrawingSegment(cSegment xSegment, int xIndex) {

      this.mSegment = xSegment;
      this.mIndex = xIndex;

    }

  }

}

