using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawShape {

  public class cDrawingSegment {

    private cSegment mSegment;

    internal cSegment Segment { get { return mSegment; } set { mSegment = value; } }

    public cDrawingSegment(cSegment xSegment) {

      this.mSegment = xSegment;

    }
  }

}

